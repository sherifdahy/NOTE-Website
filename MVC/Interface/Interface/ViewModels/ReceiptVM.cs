using System.ComponentModel.DataAnnotations;

namespace Interface.ViewModels
{
    public class ReceiptVM
    {
        [Required]
        public string ReceiptNumber { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string IssuedNumber { get; set; }
        [Required]
        public string IssuedName { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0,1000000)]
        public decimal Quantity { get; set; }
        [Required]
        [Range(0,1000000)]
        
        public decimal UnitPrice { get; set; }
        [Required]

        [Range(0,100000)]
        public decimal Discount { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
