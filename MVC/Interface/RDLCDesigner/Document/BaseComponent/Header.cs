using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Document.BaseComponent
{
    [JsonObject]
    public class Header
    {
        DateTime _dataTimeIssued;
        [JsonIgnore]
        public int HeaderId { get; set; }
        public DateTime DateTimeIssued { get { return _dataTimeIssued; } set { _dataTimeIssued = value; } }
        public string ReceiptNumber { get; set; }
        public string Uuid { get; set; }
        public string PreviousUUID { get; set; }
        public string ReferenceOldUUID { get; set; }
        public string ReferenceUUID { get; set; }
        public string Currency { get; set; }
        public int ExchangeRate { get; set; }
        public string SOrderNameCode { get; set; }
        public string OrderdeliveryMode { get; set; }

        

    }
}
