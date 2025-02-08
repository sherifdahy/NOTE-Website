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

        public Product CastingToModel(int id,int prodId = 0)
        {
            return new Product {
                Id = prodId,
                Name = Name,
                Description = Description,
                Code = Code,
                CodeType = CodeType,
                UnitPrice = UnitPrice,
                UnitType = UnitType,
                ApplicationUserId = id,
                InternalId = InternalId


            };
        }
        public ProductVM CastingFromModel(Product product)
        {
            return new ProductVM
            {
                Name = product.Name,
                Description = product.Description,
                Code = product.Code,
                CodeType = product.CodeType,
                InternalId= product.InternalId,
                UnitPrice= product.UnitPrice,
                UnitType= product.UnitType,
            };
        }
    }
}
