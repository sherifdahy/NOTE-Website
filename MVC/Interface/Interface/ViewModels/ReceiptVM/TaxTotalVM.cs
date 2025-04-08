using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ViewModels.ReceiptVM
{
    public class TaxTotalVM
    {
        decimal _amount;
        public string TaxType { get; set; }
        
        public decimal Amount  {
            set {
                _amount = Math.Abs(value);
            }
            get {
                return _amount;
            }

        }
    }
}
