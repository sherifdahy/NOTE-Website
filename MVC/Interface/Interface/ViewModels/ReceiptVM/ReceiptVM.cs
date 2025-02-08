using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ViewModels.ReceiptVM
{
    public class ReceiptVM
    {
        [Required]
        public string ReceiptNumber { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string IssuedName {  get; set; }
        [Required]
        public string IssuedNumber { get; set; }
        [Required]
        public  ICollection<ItemVM> itemData { get; set; }
        [Required]
        public decimal TotalSales { get; set; }
        
        public decimal TotalCommercialDiscount { get; set; }
        [Required]
        public decimal NetAmount { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public  ICollection<TaxTotalVM> TaxTotals { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
    }
}
