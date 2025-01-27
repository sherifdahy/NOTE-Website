using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ApplicationUser:IdentityUser<int>
    {
        
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
