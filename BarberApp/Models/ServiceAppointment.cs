namespace BarberApp.Models
{
    public class ServiceAppointment
    {
        public int AppointmentID{ get; set; }
        public Appointment Appointment { get; set; }
        public int ServiceID { get; set; }
        public Service Service{ get; set; }
    }
}
