using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Brewery_App_Backend.Entities
{
    public class User
    {
        [Key]
        public Guid AuthId { get; set; }
        [Required]
        public string UserName { get; set; } = null!;
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;

        [Required]
        public string? Password { get; set; }
        
    }
}
