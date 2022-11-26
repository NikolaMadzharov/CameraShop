using System.ComponentModel.DataAnnotations;


namespace CameraShop.Core.Models.Account
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 9)]
        [Display(Name = "Email")]
        public string Email { get; set; }




    }
}
