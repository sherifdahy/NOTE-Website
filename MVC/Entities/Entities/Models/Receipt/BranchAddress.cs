using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    public class BranchAddress
    {
        public int BranchAddressId { get; set; }
        public string Country { get; set; }
        public string Governate { get; set; }
        public string RegionCity { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }

        
    }
}
