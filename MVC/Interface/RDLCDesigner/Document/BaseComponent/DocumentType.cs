using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Document.BaseComponent
{
    [JsonObject]
    public class DocumentType
    {
        [JsonIgnore]
        public int DocumentTypeId { get; set; }
        public string ReceiptType { get; set; }
        public string TypeVersion { get; set; }
    }
}
