using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    [JsonObject]

    public class item
    {
        [JsonIgnore]
        public int itemId { get; set; }
        public string internalCode { get; set; }
        public string description { get; set; }
        public string itemType { get; set; }
        public string itemCode { get; set; }
        public string unitType { get; set; }
        public decimal quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal netSale { get; set; }
        public decimal totalSale { get; set; }
        public decimal total { get; set; }
        public virtual ICollection<taxableItem> taxableItems { get; set; } = new HashSet<taxableItem>();
        public virtual ICollection<commercialDiscountData> commercialDiscountData { get; set; } = new HashSet<commercialDiscountData>();

    }
}
