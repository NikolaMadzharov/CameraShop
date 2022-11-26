using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CameraShop.Core.Models.Account
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
