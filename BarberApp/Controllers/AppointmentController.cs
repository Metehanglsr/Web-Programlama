using BarberApp.Models;
using BarberApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly BarberDbContext _context;
        public AppointmentController(BarberDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string? date)
        {
            var barbers = _context.Barbers.Include(x=>x.Schedules)
                                          .Include(x=>x.Appointments)
                                          .ToList();
            var services = _context.Services.ToList();
            if (date != null) 
            {
                ViewBag.SelectedDate = DateTime.Parse(date);
            }
            var barberServiceViewModel = new ServiceBarberViewModel { Services = services,Barbers = barbers };
            return View(barberServiceViewModel);
        }
        public IActionResult IncreaseDate(string? date)
        {
            DateTime today = DateTime.Now;
            DateTime newDate = DateTime.Parse(date);
            newDate = newDate.AddDays(1);
            if(newDate>today.AddDays(10))
            {
                TempData["msg"] = "10 Günden ilerisine randevu oluşturulamaz.";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", new { date = newDate.ToString("yyyy-MM-dd") });
        }
        public IActionResult DecreaseDate(string? date)
        {
            DateTime today = DateTime.Now;
            DateTime newDate = DateTime.Parse(date);
            newDate = newDate.AddDays(-1);
            if (newDate < today)
            {
                TempData["msg"] = "Geçmiş tarihe randevu oluşturulamaz.";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", new { date = newDate.ToString("yyyy-MM-dd") });
        }
        [HttpPost]
        public IActionResult RandevuOlustur(DateOnly selectedDate, string selectedTime, int BarberID,List<int> ServiceID,int CustomerID)
        {
            if(ServiceID.Count==0)
            {
                TempData["msg"] = "Hizmetlerden en az bir tanesi seçilmelidir!";
                return RedirectToAction("Index");
            }
            DateTime date = selectedDate.ToDateTime(TimeOnly.Parse(selectedTime));

            Appointment appointment = new Appointment
            {
                AppointmentDate = date,
                BarberID = BarberID,
                CustomerID = CustomerID,
                Status = AppointmentStatus.Pending
            };
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            foreach (var serviceID in ServiceID)
            {
                _context.ServiceAppointments.Add(new ServiceAppointment
                {
                    AppointmentID = appointment.AppointmentID,
                    ServiceID = serviceID
                });
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
