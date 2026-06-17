using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CheckoutDto
    {
    
            public string Phone { get; set; } = string.Empty;

            public string Address { get; set; } = string.Empty;

            public string? PaymentMethod { get; set; }

            public string City { get; set; } = string.Empty;
        }
    }

