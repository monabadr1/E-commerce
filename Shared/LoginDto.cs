using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class LoginDto
    {
        [Required(ErrorMessage="Email address is required.")]
        [EmailAddress (ErrorMessage ="Invalid email address format.")]
        public string Email { get; set; }=string.Empty;
        [Required (ErrorMessage ="Password is required.")]
        [DataType(DataType.Password)]
        public string Password {  get; set; }=string.Empty;

    }
}
