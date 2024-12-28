using BarberApp.Models;
using BarberApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly BarberDbContext _context;
        private readonly UserManager<User> _adminManager;

        public AdminController(BarberDbContext context, UserManager<User> adminManager)
        {
            _context = context;
            _adminManager = adminManager;
        }
        public async Task<IActionResult> Index()
        {
            var barbers = _context.Barbers.Include(x=>x.Schedules).ToList();
            var expanses = _context.Expanses.ToList();
            var user = await _adminManager.FindByNameAsync(User.Identity.Name);
            var barberExpanse = new BarberExpanseViewModel
            {
                Barbers = barbers,
                Expanses = expanses,
                user = user
            };
            return View(barberExpanse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.Name,
                    LastName = model.Surname,
                    PhoneNumber = model.PhoneNumber,
                    isAdmin = true
                };

                var result = await _adminManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _adminManager.AddToRoleAsync(user, "Admin");
                    TempData["msg"] = "Admin is added succesfuly";
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View(model);
        }
        public IActionResult ManageAdmins()
        {
            var admins = _context.Users.Where(x=>x.isAdmin == true).ToList();
            return View(admins);
        }
        public IActionResult CreateAdmin()
        {
            return View();
        }
        public IActionResult DeleteAdmin(int id)
        {
            var admin = _context.Users.Find(id);
            if (admin == null)
            {
                TempData["msg"] = "Admin not found.";
                return RedirectToAction("ManageAdmins");
            }
            if(_context.Users.Where(x=>x.isAdmin == true).ToList().Count<=1)
            {
                TempData["msg"] = "All admins cannot delete.";
                return RedirectToAction("ManageAdmins");
            }
            _context.Users.Remove(admin);
            _context.SaveChanges();
            TempData["msg"] = "Admin deleted successfully.";
            return RedirectToAction("ManageAdmins");
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
        public IActionResult CreateBarber(Barber barber, List<int> dayofweek, List<string> startTime, List<string> endTime)
        {
            if (ModelState.IsValid)
            {
                _context.Barbers.Add(barber);
                _context.SaveChanges();
                var _barber = _context.Barbers.Find(barber.BarberID);
                if (_barber == null)
                {
                    return View("Error");
                }
                List<Schedule> schedules = new List<Schedule>();

                for (int i = 0; i < dayofweek.Count; i++)
                {
                    int dayIndex = dayofweek[i];
                    if (Enum.IsDefined(typeof(DayOfWeek), dayIndex))
                    {
                        schedules.Add(new Schedule
                        {
                            DayOfWeek = (DayOfWeek)dayIndex,
                            StartTime = TimeOnly.Parse(startTime[i]),
                            EndTime = TimeOnly.Parse(endTime[i]),
                            BarberID = _barber.BarberID
                        });
                    }
                }

                _context.Schedules.AddRange(schedules);
                _context.SaveChanges();
                return RedirectToAction("ManageBarbers");
            }

            return View();
        }



        public IActionResult EditBarber(int id)
        {
            var barber = _context.Barbers.Include(x => x.Schedules).FirstOrDefault(x => x.BarberID == id);
            return View(barber);
        }

        [HttpPost]
        public IActionResult EditBarber(Barber barber, List<int> dayofweek, List<string> startTime, List<string> endTime)
        {
            if (ModelState.IsValid)
            {

                var existingSchedules = _context.Schedules.Where(s => s.BarberID == barber.BarberID).ToList();
                _context.Schedules.RemoveRange(existingSchedules);
                _context.SaveChanges();

                _context.Barbers.Update(barber);
                _context.SaveChanges();

                var _barber = _context.Barbers.Find(barber.BarberID);
                if (_barber == null)
                {
                    return View("Error");
                }

                List<Schedule> schedules = new List<Schedule>();

                for (int i = 0; i < dayofweek.Count; i++)
                {
                    int dayIndex = dayofweek[i];
                    if (Enum.IsDefined(typeof(DayOfWeek), dayIndex))
                    {
                        schedules.Add(new Schedule
                        {
                            DayOfWeek = (DayOfWeek)dayIndex,
                            StartTime = TimeOnly.Parse(startTime[i]),
                            EndTime = TimeOnly.Parse(endTime[i]),
                            BarberID = _barber.BarberID
                        });
                    }
                }

                _context.Schedules.AddRange(schedules);
                _context.SaveChanges();

                TempData["msg"] = $"The barber named {barber.Name} has been successfully updated.";
                return RedirectToAction("ManageBarbers");
            }

            return View(barber);
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
                    if (item.AppointmentDate.Date >= today.Date)
                    {
                        TempData["msg"] = $"The barber named {barber.Name} could not be deleted because they have an appointment.";
                        return RedirectToAction("ManageBarbers");
                    }
                }
                _context.Barbers.Remove(barber);
                _context.SaveChanges();
                TempData["msg"] = $"The barber named {barber.Name} has been successfully deleted.";
            }
            else
            {
                TempData["msg"] = $"An error occurred while deleting the barber with ID {id}.";
            }
            return RedirectToAction("ManageBarbers");
        }

        public IActionResult ManageAppointments()
        {
            var appointments = _context.Appointments
                                     .Include(x => x.Barber)
                                     .Include(x => x.Customer)
                                     .AsEnumerable()
                                     .Select(a =>
                                     {
                                         a.AppointmentDate = a.AppointmentDate.ToLocalTime();
                                         return a;
                                     })
                                     .ToList();
            return View(appointments);
        }

        [HttpPost]
        public IActionResult ChangeAppointmentStatus(int appointmentId, int status)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.AppointmentID == appointmentId);
            if (appointment == null)
            {
                TempData["msg"] = "Randevu bulunamadı.";
                return RedirectToAction("ManageAppointments");
            }

            if (Enum.IsDefined(typeof(AppointmentStatus), status))
            {
                appointment.Status = (AppointmentStatus)status;
                _context.SaveChanges();
                TempData["msg"] = $"Appointment status is successfully set to {(AppointmentStatus)status}.";
            }

            return RedirectToAction("ManageAppointments");
        }

        [HttpPost]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _context.Appointments
                                       .Include(a => a.ServiceAppointments)
                                       .FirstOrDefault(a => a.AppointmentID == id);

            if (appointment == null)
            {
                TempData["msg"] = "Appointment doesnt exist.";
                return RedirectToAction("ManageAppointments");
            }

            _context.ServiceAppointments.RemoveRange(appointment.ServiceAppointments);

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            TempData["msg"] = "Appointment deleted succesfully";
            return RedirectToAction("ManageAppointments");
        }

        public IActionResult ManageServices()
        {
            var services = _context.Services
                          .Include(s => s.Category)
                          .ToList();
            return View(services);
        }

        public IActionResult CreateService()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpPost]
        public IActionResult CreateService(Service model)
        {
            if (ModelState.IsValid)
            {
                var category = _context.Categories.Find(model.CategoryID);
                model.Category = category;
                _context.Services.Add(model);
                _context.SaveChanges();
                TempData["msg"] = "Service created successfully.";
                return RedirectToAction("ManageServices");
            }

            var categories = _context.Categories.ToList();
            ViewBag.Errors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
            return View(categories);
        }



        public IActionResult EditService(int id)
        {
            var service = _context.Services.Include(x => x.Category).FirstOrDefault(x => x.ServiceID == id);
            var categories = _context.Categories.ToList();
            List<Service> serviceList = new List<Service>();
            serviceList.Add(service); ;
            ServiceCategoryViewModel serviceCategoryViewModel = new ServiceCategoryViewModel
            {
                Services = serviceList,
                Categories = categories
            };
            return View(serviceCategoryViewModel);
        }

        [HttpPost]
        public IActionResult EditService(Service model)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Update(model);
                _context.SaveChanges();
                TempData["msg"] = "Service updated successfully.";
                return RedirectToAction("ManageServices");
            }
            return View(model);
        }

        public IActionResult DeleteService(int id)
        {
            var service = _context.Services
                                  .Include(s => s.ServiceAppointments)
                                  .FirstOrDefault(s => s.ServiceID == id);

            if (service == null)
            {
                TempData["msg"] = "Service not found.";
                return RedirectToAction("ManageServices");
            }

            if (service.ServiceAppointments.Any())
            {
                TempData["msg"] = "Service cannot be deleted because it is associated with appointments.";
                return RedirectToAction("ManageServices");
            }

            _context.Services.Remove(service);
            _context.SaveChanges();
            TempData["msg"] = "Service deleted successfully.";
            return RedirectToAction("ManageServices");
        }


        [HttpGet]
        [Route("Admin/GetAppointmentsByDateAndBarber")]
        public IActionResult GetAppointmentsByDateAndBarber(int barberId, DateTime date)
        {
            // Gelen tarihin başlangıç ve bitiş zamanlarını hesapla
            var startDateUtc = date.Date.ToUniversalTime();
            var endDateUtc = startDateUtc.AddDays(1);

            var appointments = _context.Appointments
                                       .Include(x => x.Customer)
                                       .Include(x => x.ServiceAppointments)
                                           .ThenInclude(x => x.Service)
                                       .Where(x => x.BarberID == barberId &&
                                                   x.AppointmentDate >= startDateUtc &&
                                                   x.AppointmentDate < endDateUtc)
                                       .ToList();

            if (!appointments.Any())
            {
                return NoContent();
            }

            var totalEarnings = appointments
                                 .SelectMany(a => a.ServiceAppointments)
                                 .Sum(sa => sa.Service.Price);

            // Verileri formatlayın
            var formattedAppointments = appointments.Select(a => new
            {
                a.AppointmentDate,
                a.Status,
                Customer = new
                {
                    a.Customer.FirstName,
                    a.Customer.LastName
                },
                ServiceAppointments = a.ServiceAppointments.Select(sa => new
                {
                    ServiceName = sa.Service != null ? sa.Service.Name : "Unknown Service",  // Service ismi kontrol edilerek alındı
                    ServicePrice = sa.Service != null ? sa.Service.Price : 0.00  // Service fiyatı kontrol edilerek alındı
                }).ToList()  // ServiceAppointments listesinin her elemanı için döngü oluşturuluyor
            }).ToList();

            var result = new
            {
                Appointments = formattedAppointments,
                DailyEarnings = totalEarnings
            };

            return Json(result);
        }


    }
}