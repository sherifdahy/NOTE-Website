using Entities.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interface.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength (100)]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

        public  ApplicationUser Casting()
        {
            return new ApplicationUser()
            {
                UserName = this.Username,
                Email = this.Email,
            };


        }
    }
}
