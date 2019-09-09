using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gaska.Models;
using Microsoft.AspNetCore.Identity;

namespace Gaska.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // Dashboard
        public IActionResult Index()
        {
            var currentUserId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
        
        public IActionResult FuelCalculator(FuelCalculatorViewModel model)
        {
            var currentUserId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
