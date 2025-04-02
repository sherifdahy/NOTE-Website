using Entities.Models.Document.BaseComponent;
using Entities.Models.Document.Receipt;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ApplicationUser:IdentityUser<int>
    {
        public virtual ICollection<Receipt> Receipts { get; set; } = new HashSet<Receipt>();
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public string? Name { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Country { get; set; }
        public string? Governate { get; set; }
        public string? RegionCity { get; set; }
        public string? Street { get; set; }
        public string? BuildingNumber { get; set; }
        public string? Mobile { get; set; }
        public string? POSSerial { get; set; }
        public string? POSClientId { get; set; }
        public string? POSClientSecret { get; set; }
        public string? ActivityCodes { get; set; }
        public string? BranchCode { get; set; }

    }
}
