using System.ComponentModel.DataAnnotations;

namespace HohoCarApp.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastLoginAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public string Role { get; set; } = "User";
        
        public string FullName => $"{FirstName} {LastName}".Trim();
        public int ViewsCount { get; set; } = 0;
    }
}
