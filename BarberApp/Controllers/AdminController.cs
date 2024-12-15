using BarberApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly BarberDbContext _context;
        public AdminController(BarberDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var admin1 = _context.Admins.FirstOrDefault() ?? throw new Exception("Admin bulunamadı. Lütfen bir admin kaydı ekleyin.");
            _context.Barbers.ToList().ForEach(item => item.AdminID = admin1.AdminID);
            _context.Services.ToList().ForEach(item => item.AdminID = admin1.AdminID);
            _context.Appointments.ToList().ForEach(item => item.AdminID = admin1.AdminID);
            _context.Expanses.ToList().ForEach(item => item.AdminID = admin1.AdminID);
            _context.SaveChanges();
            var admin = _context.Admins.Include(x => x.Expanses)
                                       .Include(x => x.Services)
                                       .Include(x => x.Appointments)
                                       .Include(x => x.Barbers)
                                       .FirstOrDefault();
            return View(admin);
        }
    }
}
