using Microsoft.AspNetCore.Mvc;
using preciousportfolio.Data;
using preciousportfolio.Models;

namespace preciousportfolio.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = DashboardViewModel.Sample();
            return View(vm);
        }

        [HttpGet]
        public IActionResult AddHoldings()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHoldings(Holding holding)
        {
            if (!ModelState.IsValid)
            {
                return View(holding);
            }

            _context.Holdings.Add(holding);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Holding saved successfully.";
            return RedirectToAction(nameof(AddHoldings));
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
            TempData["AccountMessage"] = "Account changes saved (placeholder).";
            return RedirectToAction("Account");
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}