using BarberApp.Models;
using BarberApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly BarberDbContext _context;
        private readonly UserManager<User> _userManager;
        public AppointmentController(BarberDbContext context,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                TempData["msg"] = "Appointments cannot be made for more than 10 days in advance.";
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
                TempData["msg"] = "Appointments cannot be made for past dates.";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", new { date = newDate.ToString("yyyy-MM-dd") });
        }
        [HttpPost]
        public async Task<IActionResult> RandevuOlustur(DateOnly selectedDate, string selectedTime, int BarberID,List<int> ServiceID)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if(ServiceID.Count==0)
            {
                TempData["msg"] = "At least one service must be selected!";
                return RedirectToAction("Index");
            }
            DateTime date = selectedDate.ToDateTime(TimeOnly.Parse(selectedTime)).ToUniversalTime();

            Appointment appointment = new Appointment
            {
                AppointmentDate = date,
                BarberID = BarberID,
                CustomerID = user.Id,
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
        [HttpPost]
        public IActionResult CancelAppointment(int appointmentId)
        {
            var appointment = _context.Appointments.Find(appointmentId);
            if (appointment.Status == AppointmentStatus.Pending)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            else if(appointment.Status == AppointmentStatus.Cancelled)
            {
                TempData["msg"] = "The appointment cannot be canceled because it has been already cancelled by admin.";
            }
            else
            {
                TempData["msg"] = "The appointment cannot be canceled because it has been confirmed.";
            }
            return RedirectToAction("Profile","Account");
        }
    }
}
