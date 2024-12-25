using System.ComponentModel.DataAnnotations;

namespace BarberApp.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [Required(ErrorMessage = "Start time is required.")]
        public TimeOnly StartTime { get; set; }
        [Required(ErrorMessage = "End time is required.")]
        public TimeOnly EndTime { get; set; }
        public int BarberID { get; set; }
        public Barber? Barber { get; set; }
    }
}
