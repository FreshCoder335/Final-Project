using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preciousportfolio.Data;
using preciousportfolio.Models;
using System.Security.Claims;

namespace preciousportfolio.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Holdings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var holdings = await _context.Holdings
                .Where(h => h.UserId == userId)
                .OrderBy(h => h.MetalType)
                .ThenBy(h => h.Description)
                .ToListAsync();

            return View(holdings);
        }

        [HttpGet]
        public IActionResult AddHoldings()
        {
            return View(new Holding());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHoldings(Holding holding)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return Challenge();
            }

            holding.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ModelState.Remove(nameof(Holding.UserId));
            ModelState.Remove(nameof(Holding.User));

            if (!ModelState.IsValid)
            {
                return View(holding);
            }

            _context.Holdings.Add(holding);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Holding saved successfully.";
            return RedirectToAction(nameof(Holdings));
        }

        [HttpGet]
        public async Task<IActionResult> EditHolding(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var holding = await _context.Holdings
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

            if (holding == null)
            {
                return NotFound();
            }

            return View(holding);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHolding(int id, Holding holding)
        {
            if (id != holding.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingHolding = await _context.Holdings
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

            if (existingHolding == null)
            {
                return NotFound();
            }

            holding.UserId = userId;

            ModelState.Remove(nameof(Holding.UserId));
            ModelState.Remove(nameof(Holding.User));

            if (!ModelState.IsValid)
            {
                return View(holding);
            }

            existingHolding.MetalType = holding.MetalType;
            existingHolding.FormType = holding.FormType;
            existingHolding.Description = holding.Description;
            existingHolding.WeightOz = holding.WeightOz;
            existingHolding.Purity = holding.Purity;
            existingHolding.Quantity = holding.Quantity;
            existingHolding.AcquiredDate = holding.AcquiredDate;
            existingHolding.AcquiredPrice = holding.AcquiredPrice;
            existingHolding.Dealer = holding.Dealer;
            existingHolding.StorageLocation = holding.StorageLocation;
            existingHolding.Notes = holding.Notes;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Holding updated successfully.";
            return RedirectToAction(nameof(Holdings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteHolding(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var holding = await _context.Holdings
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

            if (holding == null)
            {
                return NotFound();
            }

            _context.Holdings.Remove(holding);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Holding deleted successfully.";
            return RedirectToAction(nameof(Holdings));
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