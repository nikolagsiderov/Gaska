﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gaska.Data.Models;
using Gaska.Data;
using Microsoft.AspNetCore.Identity;
using Gaska.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gaska.Controllers
{
    public class ServiceBookLogsController : Controller
    {
        private readonly DataAccessContext _context;
        private UserManager<ApplicationUser> userManager;

        public ServiceBookLogsController(DataAccessContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: ServiceBookLogs
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUserId = userManager.GetUserId(User);

            return View(await _context.ServiceBookLogs.ToListAsync());
        }

        // GET: ServiceBookLogs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var currentUserId = userManager.GetUserId(User);

            if (id == null)
            {
                return NotFound();
            }

            var serviceBookLog = await _context.ServiceBookLogs
                .FirstOrDefaultAsync(m => m.ServiceBookLogId == id);
            if (serviceBookLog == null)
            {
                return NotFound();
            }

            return View(serviceBookLog);
        }

        // GET: ServiceBookLogs/Create
        [Authorize]
        public IActionResult Create(int? id)
        {
            var currentUserId = userManager.GetUserId(User);

            return View();
        }

        // POST: ServiceBookLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceBookLogId,Date,Mileage,ServiceType,NextServiceDate,NextServiceMileage,CarWork,Details")] ServiceBookLog serviceBookLog)
        {
            var currentUserId = userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                _context.Add(serviceBookLog);
                await _context.SaveChangesAsync();

                int? id = int.Parse(RouteData.Values["id"].ToString());

                if (id != null)
                {
                    var serviceBooks = new ServiceBook[]
                    {
                    new ServiceBook{CarId=(int)id, ServiceBookLogId= serviceBookLog.ServiceBookLogId}
                    };

                    foreach (ServiceBook serviceBook in serviceBooks)
                    {
                        _context.ServiceBooks.Add(serviceBook);
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Details", "Cars", new { id });
            }
            return View(serviceBookLog);
        }

        // GET: ServiceBookLogs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var currentUserId = userManager.GetUserId(User);

            if (id == null)
            {
                return NotFound();
            }

            var serviceBookLog = await _context.ServiceBookLogs.FindAsync(id);
            if (serviceBookLog == null)
            {
                return NotFound();
            }
            return View(serviceBookLog);
        }

        // POST: ServiceBookLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceBookLogId,Date,Mileage,ServiceType,NextServiceDate,NextServiceMileage,CarWork,Details")] ServiceBookLog serviceBookLog)
        {
            var currentUserId = userManager.GetUserId(User);

            if (id != serviceBookLog.ServiceBookLogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceBookLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceBookLogExists(serviceBookLog.ServiceBookLogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                var carId = _context.ServiceBooks.Where(x => x.ServiceBookId == id).Select(x => x.CarId).First();
                id = carId;
                
                return RedirectToAction("Details", "Cars", new { id });
            }
            return View(serviceBookLog);
        }

        // GET: ServiceBookLogs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var currentUserId = userManager.GetUserId(User);

            if (id == null)
            {
                return NotFound();
            }

            var serviceBookLog = await _context.ServiceBookLogs
                .FirstOrDefaultAsync(m => m.ServiceBookLogId == id);
            if (serviceBookLog == null)
            {
                return NotFound();
            }

            return View(serviceBookLog);
        }

        // POST: ServiceBookLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUserId = userManager.GetUserId(User);

            var carId = _context.ServiceBooks.Where(x => x.ServiceBookId == id).Select(x => x.CarId).First();

            var serviceBookLog = await _context.ServiceBookLogs.FindAsync(id);
            _context.ServiceBookLogs.Remove(serviceBookLog);
            await _context.SaveChangesAsync();

            id = carId;

            return RedirectToAction("Details", "Cars", new { id });
        }

        private bool ServiceBookLogExists(int id)
        {
            return _context.ServiceBookLogs.Any(e => e.ServiceBookLogId == id);
        }
    }
}
