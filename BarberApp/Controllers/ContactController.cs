using BarberApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly BarberDbContext _context;
        public ContactController(BarberDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var barbers = _context.Barbers.ToList();
            return View(barbers);
        }
    }
}