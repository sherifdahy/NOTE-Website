using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{

    [JsonObject]
    public class taxTotal
    {
        [JsonIgnore]
        public int taxTotalId { get; set; }
        public string taxType { get; set; }
        public decimal amount { get; set; }
    }
}
