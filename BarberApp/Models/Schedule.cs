using System.ComponentModel.DataAnnotations;

namespace BarberApp.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int BarberID { get; set; }
        public Barber Barber { get; set; }
    }
}
