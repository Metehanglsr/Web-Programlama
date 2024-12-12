using BarberApp.Models;
using BarberApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Controllers
{
    public class ServicesController : Controller
    {
        private readonly BarberDbContext _context;
        public ServicesController(BarberDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id)
        {
            List<Service> services = new List<Service>();
            var categories = _context.Categories.ToList();
            if (id != null)
            {
                services = _context.Services.Include(x => x.Category).Where(x => x.CategoryID == id).ToList();
            }
            else
            {
                services = _context.Services.Include(x => x.Category).ToList();
            }
            var ServiceCategoruViewModel = new ServiceCategoryViewModel
            {
                Categories = categories,
                Services = services
            };
            return View(ServiceCategoruViewModel);
        }
    }
}