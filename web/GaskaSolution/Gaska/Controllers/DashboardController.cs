﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gaska.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Gaska.Controllers
{
    public class DashboardController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public DashboardController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // Dashboard
        [Authorize]
        public IActionResult Index()
        {
            var currentUserId = userManager.GetUserId(User);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
