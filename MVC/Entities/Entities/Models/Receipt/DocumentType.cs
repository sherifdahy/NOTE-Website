using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    public class DocumentType
    {
        public int DocumentTypeId { get; set; }
        public string ReceiptType { get; set; }
        public string TypeVersion { get; set; }
    }
}
