using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{

    [JsonObject]
    public class seller
    {
        [JsonIgnore]
        public int sellerId { get; set; }
        public string rin { get; set; }
        public string companyTradeName { get; set; }
        public string branchCode { get; set; }
        [JsonIgnore]
        [ForeignKey("branchAddress")]
        public int branchAddressId { get; set; }
        public virtual branchAddress branchAddress { get; set; } 
        public string deviceSerialNumber { get; set; }
        public string activityCode { get; set; }
    }
}
