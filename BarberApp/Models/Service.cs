using System.ComponentModel.DataAnnotations;

namespace BarberApp.Models
{
    public class Service
    {
        [Key]
        public int ServiceID{ get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }
        public float Price{ get; set; }
        public string Duration { get; set; }
        public string? ImageUrl { get; set; }
        public List<ServiceAppointment>? ServiceAppointments { get; set; }
        public int CategoryID { get; set; }
        public Category? Category{ get; set; }
    }
}
