using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gaska.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gaska.Controllers
{
    public class FuelCalculatorController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public FuelCalculatorController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index(FuelCalculatorViewModel model)
        {
            var currentUserId = userManager.GetUserId(User);

            return View();
        }
    }
}