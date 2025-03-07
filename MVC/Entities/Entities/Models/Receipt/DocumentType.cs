using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    [JsonObject]
    public class documentType
    {
        string _receiptType;

        [JsonIgnore]
        public int documentTypeId { get; set; }
        public string receiptType 
        {
            get { return _receiptType; }
            set { _receiptType = value; }
        }
        public string typeVersion { get; set; }
    }
}
