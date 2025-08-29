using System.ComponentModel.DataAnnotations;

namespace HohoCarApp.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; } = new User();
        
        public int CarId { get; set; }
        public Car Car { get; set; } = new Car();
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
