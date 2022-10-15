namespace TechRentingSystem.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Emal { get; set; }
        [Required]
        [Compare(nameof(PasswordConfirmation))]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirmation { get; set; }
        [Required]
        public string FIrstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
