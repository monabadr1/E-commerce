using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CheckoutDto
    {
        [Required(ErrorMessage ="Phone number is required.")]
        [Phone(ErrorMessage ="Invalid phone number format.")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Please enter a valid Egyptian phone number (e.g. 01012345678).")]
    
        public string Phone { get; set; } = string.Empty;
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters.")]
        public string Address { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please select a payment method.")]
        public string? PaymentMethod { get; set; } = "CashOnDelivery";
        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "City name must be between 2 and 50 characters.")]
        public string City { get; set; } = string.Empty;
        }
    }

