using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    [JsonObject]
    public class buyer
    {
        [JsonIgnore]
        [Key]
        public int buyerId { get; set; }
        public string id { get; set; }
        [Required]
        public string type { get; set; }
        [Required]
        public string name { get; set; }
        public string mobileNumber { get; set; }
        public string paymentNumber { get; set; }
    }
}
