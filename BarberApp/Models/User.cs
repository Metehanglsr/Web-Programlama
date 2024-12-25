using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BarberApp.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public bool isAdmin { get; set; }
        public List<Appointment> Appointments{ get; set; }
        public List<Review> Reviews{ get; set; }
    }
}
