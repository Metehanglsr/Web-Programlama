using BarberApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BarberApp.Controllers
{
    public class BarbersController : Controller
    {
        private readonly BarberDbContext _context;
        private readonly UserManager<User> _userManager;
        public BarbersController(BarberDbContext context,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(string? sortBy)
        {
            List<Barber> barbers;
            if(sortBy != null)
            {
                if(sortBy == "name")
                {
                     barbers = _context.Barbers.ToList().OrderByDescending(x=>x.Name).ToList();
                    ViewBag.SortBy = "name";
                    return View(barbers);
                }
                if (sortBy == "rating")
                {
                     barbers = _context.Barbers.ToList().OrderByDescending(x =>
                                            {
                                                int rating;
                                                return int.TryParse(x.Rating, out rating) ? rating : 0;
                                            }).ToList();
                    ViewBag.SortBy = "rating";
                    return View(barbers);
                }
            }
            barbers = _context.Barbers.ToList();
            return View(barbers);
        }
        public IActionResult Details(int id)
        {
            var barber = _context.Barbers.Include(x=>x.Schedules).Include(x=>x.Reviews).ThenInclude(x=>x.Customer).FirstOrDefault(x=>x.BarberID == id);
            return View(barber);
        }
        public async Task<IActionResult> AddComment(int id,string commentText)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return Redirect("/Barbers/Index");
            }
            _context.Reviews.Add(new Review
            {
                BarberID = id,
                Comment = commentText,
                ReviewDate = DateTime.UtcNow,
                CustomerID = user.Id
            });
            _context.SaveChanges();
            return Redirect("Details/"+id);
        }
    }
}