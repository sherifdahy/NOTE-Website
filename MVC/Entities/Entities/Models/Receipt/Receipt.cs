using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    public class Receipt
    {
        public int ReceiptId { get; set; }

        [ForeignKey("ApplicationUser")]
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Header")]
        public int HeaderId { get; set; }
        public virtual Header Header {  get; set; }

        [ForeignKey("DocumentType")]
        public int DocumentTypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }

        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public virtual Seller Seller { get; set; }

        public virtual ICollection<Item> itemData { get; set; } = new HashSet<Item>();
        public decimal TotalSales { get; set; }
        public decimal TotalCommercialDiscount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<TaxTotal> TaxTotals { get; set; } = new HashSet<TaxTotal>();
        public string PaymentMethod { get; set; }
    }
}
