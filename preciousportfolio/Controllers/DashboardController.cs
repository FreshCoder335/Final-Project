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
    }
}
