using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    [JsonObject]
    public class header
    {
        DateTime _dataTimeIssued;
        [JsonIgnore]
        public int headerId { get; set; }
        public DateTime dateTimeIssued { get { return _dataTimeIssued; } set { _dataTimeIssued = value; } }
        public string receiptNumber { get; set; }
        public string uuid { get; set; }
        public string previousUUID { get; set; }
        public string referenceOldUUID { get; set; }
        public string? referenceUUID { get; set; }
        public string currency { get; set; }
        public int exchangeRate { get; set; }
        public string sOrderNameCode { get; set; }
        public string orderdeliveryMode { get; set; }

        

    }
}
