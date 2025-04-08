using Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [Required(ErrorMessage = "يجب ادخال رقم الايصال")]
        [RegularExpression(@"\d{1,6}",ErrorMessage = "جيب ان يكون رقم الايصال ارقام فقط ولا يزيد عن 6 ارقام")]
        public string ReceiptNumber { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Version { get; set; }
        [Required(ErrorMessage ="يجب ادخال اسم العميل")]
        public string IssuedName { get; set; }

        public string? IssuedNumber { get; set; }

        public string IssuedType { get; set; }
        public string? IssuedPhone { get; set; }

        [Required (ErrorMessage = "لا يمكن تقديم الايصال فارغ من البنود")]
        public List<ItemVM> itemData { get; set; }
        [Required]
        public decimal TotalSales { get; set; }

        public decimal TotalCommercialDiscount { get; set; }
        [Required]
        public decimal NetAmount { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        [ModelBinder(BinderType = typeof(TaxTotalsVMModelBinder))]
        public List<TaxTotalVM> TaxTotals { get; set; }
        [Required]
        public string PaymentMethod { get; set; }


    }
    
}