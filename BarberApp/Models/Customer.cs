using System.ComponentModel.DataAnnotations;

namespace BarberApp.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID{ get; set; }
        public string Name{ get; set; }
        public string Surname{ get; set; }
        [EmailAddress]
        public string Email{ get; set; }
        [Phone]
        public string PhoneNumber{ get; set; }
        public List<Appointment> Appointments{ get; set; }
        public List<Review> Reviews{ get; set; }
    }
}
