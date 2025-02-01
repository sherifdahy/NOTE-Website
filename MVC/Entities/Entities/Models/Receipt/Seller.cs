using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    public class Seller
    {
        public int SellerId { get; set; }
        public string Rin { get; set; }
        public string CompanyTradeName { get; set; }
        public string BranchCode { get; set; }
        public virtual BranchAddress BranchAddress { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string ActivityCode { get; set; }
    }
}
