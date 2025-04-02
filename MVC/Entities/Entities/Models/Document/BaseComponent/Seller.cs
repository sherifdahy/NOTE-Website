using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Document.BaseComponent
{

    [JsonObject]
    public class Seller
    {
        [JsonIgnore]
        public int SellerId { get; set; }
        public string Rin { get; set; }
        public string CompanyTradeName { get; set; }
        public string BranchCode { get; set; }
        [JsonIgnore]
        [ForeignKey("BranchAddress")]
        public int BranchAddressId { get; set; }
        public virtual BranchAddress BranchAddress { get; set; } 
        public string DeviceSerialNumber { get; set; }
        public string ActivityCode { get; set; }
    }
}
