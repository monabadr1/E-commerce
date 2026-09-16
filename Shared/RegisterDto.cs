using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Shared
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Name is requied")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Name must be between 3 and 50 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required (ErrorMessage ="Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]

        public string Email { get; set; }= string.Empty;
        [Required (ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character (@$!%*?&#).")]
        [DataType(DataType.Password)]
        public string Password { get; set; }=string.Empty;
        [Required (ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirm Password do not match.")]
        public string Confirmpassword { get; set; } = string.Empty;

        [Phone (ErrorMessage ="Invalid phone number format.")]
        [RegularExpression (@"01[0125][0-9]{8}$", ErrorMessage = "Please enter a valid Egyptian phone number (e.g. 01012345678).")]
        public string? Phone {  get; set; }


    }
}
