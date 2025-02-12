using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    

    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string InternalId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Range(0,100000)]
        public decimal UnitPrice { get; set; }
        [StringLength(2)]
        [Required]
        public string UnitType { get; set; }
        [Required]
        [StringLength (100)]
        public string Code { get; set; }
        [Required]
        [StringLength(3)]
        public string CodeType { get; set; }

        [Required] 
        public int ApplicationUserId { get; set; }

        public virtual applicationUser ApplicationUser { get; set; }
    }
}
