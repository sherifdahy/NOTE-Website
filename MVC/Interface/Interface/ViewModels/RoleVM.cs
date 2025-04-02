using System.ComponentModel.DataAnnotations;

namespace Interface.ViewModels
{
    public class RoleVM
    {
        [Required]
        [StringLength(100)]
        
        public string Name { get; set; }
    }
}
