using System.ComponentModel.DataAnnotations;

namespace BarberApp.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        public string Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate{ get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int BarberID { get; set; }
        public Barber Barber { get; set; }

    }
}
