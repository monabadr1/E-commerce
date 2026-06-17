using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CurrentUserDto
    {
        public int UserId { get; set; }
        
        public string Name {  get; set; }=string.Empty;

        public string Email { get; set; }=string.Empty;
        
        }
    }

