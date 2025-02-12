using Entities.Models.Receipt;
using Interface.Services;
using Microsoft.AspNetCore.Mvc;
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
        public string Type { get; set; }
        [Required]
        public string Version { get; set; }
        [Required]
        public string IssuedName { get; set; }
        
        public string? IssuedNumber { get; set; }

        public string IssuedType { get; set; }
        public string? IssuedPhone { get; set; }

        [Required]
        [ModelBinder(BinderType = typeof(ItemVMModelBinder))]
        public ICollection<ItemVM> itemData { get; set; }
        [Required]
        public decimal TotalSales { get; set; }

        public decimal TotalCommercialDiscount { get; set; }
        [Required]
        public decimal NetAmount { get; set; }
        [Required]  
        public decimal TotalAmount { get; set; }
        [ModelBinder(BinderType = typeof(TaxTotalVMModelBinder))]
        public ICollection<TaxTotalVM> TaxTotals { get; set; } 
        [Required]
        public string PaymentMethod { get; set; }

        
    }
}