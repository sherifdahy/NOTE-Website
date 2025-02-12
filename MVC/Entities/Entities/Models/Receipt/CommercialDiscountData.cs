using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    [JsonObject]
    public class commercialDiscountData
    {
        [JsonIgnore]
        public int commercialDiscountDataId { get; set; }
        public decimal amount { get; set; }
        public string description { get; set; }
        public decimal rate { get; set; }
    }
}
