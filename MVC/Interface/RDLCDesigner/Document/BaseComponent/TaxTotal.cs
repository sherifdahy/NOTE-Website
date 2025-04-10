using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Document.BaseComponent
{

    [JsonObject]
    public class TaxTotal
    {
        [JsonIgnore]
        public int TaxTotalId { get; set; }
        public string TaxType { get; set; }
        public decimal Amount { get; set; }
    }
}
