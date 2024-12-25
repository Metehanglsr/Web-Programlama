using BarberApp.Models;

namespace BarberApp.ViewModels
{
    public class BarberExpanseViewModel
    {
        public List<Barber> Barbers { get; set; }
        public List<Expanse> Expanses { get; set; }
        public User user { get; set; }
    }
}
