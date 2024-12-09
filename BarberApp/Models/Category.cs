using System.ComponentModel.DataAnnotations;

namespace BarberApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryID{ get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }
        public List<Service> Services{ get; set; }
    }
}
