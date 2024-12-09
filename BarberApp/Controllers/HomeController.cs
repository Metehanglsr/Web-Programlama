using BarberApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BarberApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly BarberDbContext _context;
        public HomeController(BarberDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Customers.ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
