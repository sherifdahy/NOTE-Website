using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Document.BaseComponent
{
    [JsonObject]
    public class CommercialDiscountData
    {
        [JsonIgnore]
        public int CommercialDiscountDataId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
    }
}
