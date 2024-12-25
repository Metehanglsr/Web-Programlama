using System.ComponentModel.DataAnnotations;

namespace BarberApp.Models
{
    public class Expanse
    {
        [Key]
        public int ExpanseID { get; set; }
        public string ExpanseCategory { get; set; }
        public float ExpanseAmount { get; set; }
        public string ExpanseDescription { get; set; }
        public DateOnly ExpanseDate { get; set; }
    }
}
