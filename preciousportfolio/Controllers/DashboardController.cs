using Microsoft.AspNetCore.Mvc;
using preciousportfolio.Models;

namespace preciousportfolio.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            // This is meant to be placeholder data until I can add some real data.
            var vm = DashboardViewModel.Sample();
            return View(vm);
        }

        public IActionResult AddHoldings()
        {
            return View();
        }

        public IActionResult InventoryReport()
        {
            return View();
        }

        public IActionResult SalesReport()
        {
            return View();
        }

        public IActionResult Reports()
        {
            return View();
        }

        public IActionResult Account()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Account(string displayName, string email, string storageDefault)
        {
            // Placeholder: later persist to DB / Identity profile
            TempData["AccountMessage"] = "Account changes saved (placeholder).";
            return RedirectToAction("Account");
        }

        public IActionResult Settings()
        {
            return View();
        }

    }
}
