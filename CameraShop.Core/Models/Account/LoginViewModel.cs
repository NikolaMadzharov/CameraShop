using System.ComponentModel.DataAnnotations;

namespace CameraShop.Core.Models.Account
{
    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        public string Emal { get; set; }
        [Required]

        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
