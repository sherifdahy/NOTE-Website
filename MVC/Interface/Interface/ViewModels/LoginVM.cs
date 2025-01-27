using DAL.Data;
using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Interface.ViewModels
{
    public class LoginVM
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        
    }
}
