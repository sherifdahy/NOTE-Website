using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ViewModels.ReceiptVM
{
    public class HeaderVM
    {
        public string DateTimeIssued { get; set; }
        public string ReceiptNumber { get; set; }
        public string Uuid { get; set; }
        public string PreviousUUID { get; set; }
        public string ReferenceOldUUID { get; set; }
        public string Currency { get; set; }
        public int ExchangeRate { get; set; }
        public string SOrderNameCode { get; set; }
        public string OrderdeliveryMode { get; set; }



    }
}
