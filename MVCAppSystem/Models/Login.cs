using System.ComponentModel.DataAnnotations;

namespace MVCAppSystem.Models
{
    public class Login
    {
        [Key]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; } 
    }
}
