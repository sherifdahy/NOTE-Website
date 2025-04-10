using Entities.Models.Document.BaseComponent;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Entities.Models.Document
{
    public class Receipt
    {
        public int ReceiptId { get; set; }
        public virtual Header Header { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual Buyer Buyer { get; set; }
        public virtual IEnumerable<Item> ItemData { get; set; } = new HashSet<Item>();
        public decimal TotalSales { get; set; }
        public decimal TotalCommercialDiscount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual IEnumerable<TaxTotal> TaxTotals { get; set; } = new HashSet<TaxTotal>();
        public string PaymentMethod { get; set; }

        public string Status { get; set; }
        public string SubmissionUuid { get; set; }
        public byte[] QRCode { get; set; }

    }
}
