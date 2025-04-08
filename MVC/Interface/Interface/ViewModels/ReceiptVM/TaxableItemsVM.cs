using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ViewModels.ReceiptVM
{

    public class TaxableItemVM
    {
        decimal _amount;
        public string TaxType { get; set; }
        public string SubType { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = Math.Abs(value);
            }
        }
    }
}
