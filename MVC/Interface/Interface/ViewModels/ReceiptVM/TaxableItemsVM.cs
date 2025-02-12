using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ViewModels.ReceiptVM
{
    public class TaxableItemVM
    {
        public string TaxType { get; set; }
        public string SubType { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
