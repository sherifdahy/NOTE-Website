using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Document.BaseComponent
{
    [JsonObject]
    public class Buyer
    {
        [JsonIgnore]
        [Key]
        public int BuyerId { get; set; }
        public string Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string PaymentNumber { get; set; }
    }
}
