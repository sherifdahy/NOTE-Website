using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ViewModels.ReceiptVM
{
    public class ItemVM
    {
        public string InternalCode { get; set; }
        public string Description { get; set; }
        public string ItemType { get; set; }
        public string ItemCode { get; set; }
        public string UnitType { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NetSale { get; set; }
        public decimal TotalSale { get; set; }
        public decimal Total { get; set; }
        public virtual ICollection<TaxableItem> TaxableItems { get; set; } = new HashSet<TaxableItem>();
        public virtual ICollection<CommercialDiscountDataVM> CommercialDiscountData { get; set; } = new HashSet<CommercialDiscountDataVM>();

    }
}
