using System.ComponentModel.DataAnnotations;

namespace Server.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string firstName { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        /*[Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]*/
        public string confirmPassword { get; set; }
    }
}