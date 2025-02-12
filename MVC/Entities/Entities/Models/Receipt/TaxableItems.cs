using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    [JsonObject]
    public class taxableItem
    {
        [JsonIgnore]
        public int taxableItemId { get; set; }
        public string taxType { get; set; }
        public string subType { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }
    }
}
