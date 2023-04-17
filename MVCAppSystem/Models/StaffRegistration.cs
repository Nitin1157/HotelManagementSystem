﻿using System.ComponentModel.DataAnnotations;

namespace MVCAppSystem.Models
{
    public class StaffRegistration
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
