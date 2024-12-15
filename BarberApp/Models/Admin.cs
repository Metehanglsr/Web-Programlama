using System.ComponentModel.DataAnnotations;

namespace BarberApp.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<Barber> Barbers { get; set; }
        public List<Service> Services { get; set; }
        public List<Expanse> Expanses { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
