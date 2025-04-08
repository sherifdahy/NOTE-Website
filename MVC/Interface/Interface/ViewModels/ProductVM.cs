using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Interface.ViewModels
{
    public class ProductVM
    {
        [Required]
        [StringLength(100)]
        public string InternalId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Range(0, 100000)]
        public decimal UnitPrice { get; set; }
        [StringLength(2)]
        [Required]
        public string UnitType { get; set; }
        [Required]
        [StringLength(100)]
        public string Code { get; set; }
        [Required]
        [StringLength(3)]
        public string CodeType { get; set; }


    }
}
