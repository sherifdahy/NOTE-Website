using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    public class TaxTotal
    {
        public int TaxTotalId { get; set; }
        public string TaxType { get; set; }
        public decimal Amount { get; set; }
    }
}
