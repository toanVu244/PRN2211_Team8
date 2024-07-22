using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(16, ErrorMessage = "Username cannot be longer than 16 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(32, ErrorMessage = "Password cannot be longer than 32 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, ErrorMessage = "Full Name cannot be longer than 50 characters")]
        public string FullName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNum { get; set; }

        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters")]
        public string? Address { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters")]
        public string? Email { get; set; }
    }
}
