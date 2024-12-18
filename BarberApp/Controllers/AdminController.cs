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
                                       .ThenInclude(x => x.Schedules)
                                       .Include(x => x.Barbers)
                                       .ThenInclude(x => x.Appointments)
                                       .ThenInclude(x => x.Customer)
                                       .Include(x => x.Barbers)
                                       .ThenInclude(x => x.Appointments)
                                       .ThenInclude(x => x.ServiceAppointments)
                                       .ThenInclude(x => x.Service)
                                       .FirstOrDefault();
            return View(admin);
        }

        public IActionResult ManageBarbers()
        {
            var barbers = _context.Barbers
                                  .Include(x => x.Schedules)
                                  .ToList();
            return View(barbers);
        }

        public IActionResult CreateBarber()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBarber(Barber model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ManageBarbers");
            }
            return View(model);
        }

        public IActionResult EditBarber(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditBarber(Barber model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ManageBarbers");
            }
            return View(model);
        }

        public IActionResult DeleteBarber(int id)
        {
            var barber = _context.Barbers
                                 .Include(x => x.Appointments)
                                 .FirstOrDefault(x => x.BarberID == id);
            DateTime today = DateTime.UtcNow;
            if (barber != null)
            {
                foreach (var item in barber.Appointments)
                {
                    if(item.AppointmentDate.Date >= today.Date)
                    {
                        TempData["msg"] = $"{barber.BarberID}-{barber.Name} isimli berberin randevusu oldugu icin silinemedi.";
                        return RedirectToAction("ManageBarbers");
                    }
                }
                _context.Barbers.Remove(barber);
                _context.SaveChanges();
                TempData["msg"] = $"{barber.BarberID}-{barber.Name} isimli berber başarıyla silindi.";
            }
            else 
            {
                TempData["msg"] = $"Id'si {id} olan berber silinirken bir hata meydana geldi.";
            }
            return RedirectToAction("ManageBarbers");
        }

        public IActionResult ManageAppointments()
        {
            return View();
        }

        public IActionResult ManageAppointmentStatus(int appointmentId)
        {
            return View();
        }

        public IActionResult ManageServices()
        {
            return View();
        }

        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateService(Service model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ManageServices");
            }
            return View(model);
        }

        public IActionResult EditService(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditService(Service model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ManageServices");
            }
            return View(model);
        }

        public IActionResult DeleteService(int id)
        {
            return RedirectToAction("ManageServices");
        }


        [HttpGet]
        [Route("Admin/GetAppointmentsByDateAndBarber")]
        public IActionResult GetAppointmentsByDateAndBarber(int barberId, DateTime date)
        {
            var appointments = _context.Appointments
                                      .Include(x => x.Customer)
                                      .Include(x => x.ServiceAppointments)
                                          .ThenInclude(x => x.Service)
                                      .Where(x => x.BarberID == barberId &&
                                                  x.AppointmentDate.ToUniversalTime().Date == date.ToUniversalTime().Date)
                                      .ToList();

            if (appointments == null || appointments.Count == 0)
            {
                return NoContent();
            }

            var totalEarnings = appointments
                                .SelectMany(a => a.ServiceAppointments)
                                .Sum(sa => sa.Service.Price);

            var result = new
            {
                Appointments = appointments,
                DailyEarnings = totalEarnings
            };

            return Json(result);
        }


    }
}