using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Document.BaseComponent
{
    [JsonObject]
    public class TaxableItem
    {
        [JsonIgnore]
        public int TaxableItemId { get; set; }
        public string TaxType { get; set; }
        public string SubType { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
