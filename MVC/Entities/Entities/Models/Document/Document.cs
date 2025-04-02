using Entities.Models.Document.BaseComponent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Document
{
    [JsonObject]
    public abstract class Document
    {
            [JsonIgnore]
            public int ReceiptId { get; set; }
            [JsonIgnore]
            [ForeignKey("ApplicationUser")]
            public int ApplicationUserId { get; set; }
            [JsonIgnore]
            public virtual ApplicationUser ApplicationUser { get; set; }
            [JsonIgnore]
            [ForeignKey("Header")]
            public int HeaderId { get; set; }
            public virtual Header Header { get; set; }
            [JsonIgnore]
            [ForeignKey("DocumentType")]
            public int DocumentTypeId { get; set; }
            public virtual DocumentType DocumentType { get; set; }
            [JsonIgnore]
            [ForeignKey("Seller")]
            public int SellerId { get; set; }
            public virtual Seller Seller { get; set; }
            [JsonIgnore]
            [ForeignKey("Buyer")]
            public int BuyerId { get; set; }
            public virtual Buyer Buyer { get; set; }
            public virtual ICollection<Item> ItemData { get; set; } = new HashSet<Item>();
            public decimal TotalSales { get; set; }
            public decimal TotalCommercialDiscount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal TotalAmount { get; set; }
            public virtual ICollection<TaxTotal> TaxTotals { get; set; } = new HashSet<TaxTotal>();
            public string PaymentMethod { get; set; }

            [JsonIgnore]
            public string? Status { get; set; }
            [JsonIgnore]
            public string? SubmissionUuid { get; set; }

            [JsonIgnore]
            [NotMapped]
            public byte[] QRCode { get; set; }


    }
}
