using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gaska.Data.Models;
using Gaska.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using Gaska.Data;

namespace Gaska.Controllers
{
    public class CarsController : Controller
    {
        private readonly GaskaDbContext _context;
        private UserManager<ApplicationUser> userManager;

        public CarsController(GaskaDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Cars
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var currentUserId = userManager.GetUserId(User);

            ViewData["BrandSortParm"] = String.IsNullOrEmpty(sortOrder) ? "brand_desc" : "";
            ViewData["ModelSortParm"] = sortOrder == "Model" ? "model_desc" : "Model";
            ViewData["CurrentFilter"] = searchString;

            var cars = from s in _context.Cars.Where(x => x.UserId == currentUserId)
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(s => s.Brand.Contains(searchString)
                                       || s.Model.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "brand_desc":
                    cars = cars.OrderByDescending(s => s.Brand);
                    break;
                case "Model":
                    cars = cars.OrderBy(s => s.Model);
                    break;
                case "model_desc":
                    cars = cars.OrderByDescending(s => s.Model);
                    break;
                default:
                    cars = cars.OrderBy(s => s.Brand);
                    break;
            }
            return View(await cars.AsNoTracking().ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.ServiceBooks)
                .ThenInclude(s => s.ServiceBookLog)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CarId == id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car, IFormFile image)
        {
            var currentUserId = userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                car.UserId = currentUserId;

                if (image != null && image.Length > 0)
                {
                    var fileName = string.Empty;

                    if (car.UserId != null)
                    {
                        fileName = $"{car.Brand}_{car.Model}_{car.UserId}.{image.FileName.Split('.').Last()}";
                    }
                    else
                    {
                        fileName = $"{car.Brand}_{car.Model}_null.{image.FileName.Split('.').Last()}";
                    }
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\carimages", fileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileSteam);
                    }
                    car.Image = fileName;
                }

                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Car car, IFormFile image)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null && image.Length > 0)
                    {
                        var fileName = string.Empty;

                        if (car.UserId != null)
                        {
                            fileName = $"{car.Brand}_{car.Model}_{car.UserId}.{image.FileName.Split('.').Last()}";
                        }
                        else
                        {
                            fileName = $"{car.Brand}_{car.Model}_null.{image.FileName.Split('.').Last()}";
                        }
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\carimages", fileName);
                        using (var fileSteam = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileSteam);
                        }
                        car.Image = fileName;
                    }
                    else
                    {
                        string currentUserIdString = string.Empty;

                        if (car.UserId == null)
                        {
                            currentUserIdString = "null";
                        }
                        else
                        {
                            currentUserIdString = car.UserId.ToString();
                        }

                        foreach (var carImage in _context.Cars.Select(x => x.Image))
                        {
                            if (carImage.Split('.').First().Split('_').Last() == currentUserIdString && carImage.Split('.').First().Split('_').First() == car.Brand
                                && carImage.Split('.').First().Split('_')[1] == car.Model)
                            {
                                car.Image = carImage;
                            }
                        }
                    }

                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUserId = userManager.GetUserId(User);
            string currentUserIdString = string.Empty;

            if (currentUserId == null)
            {
                currentUserIdString = "null";
            }
            else
            {
                currentUserIdString = currentUserId.ToString();
            }

            string rootFolder = @"wwwroot\\images\\carimages\\";

            foreach (var carImage in _context.Cars.Select(x => x.Image))
            {
                if (carImage.Split('.').First().Split('_').Last() == currentUserIdString)
                {
                    try
                    {
                        System.IO.File.Delete(Path.Combine(rootFolder, carImage));
                    }
                    catch (IOException ioExp)
                    {
                    }
                }

            }

            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }
    }
}
