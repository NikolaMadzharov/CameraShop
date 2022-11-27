namespace TechRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    [Authorize]
    public class BaseController : Controller
    {
        //Create a claim and use it later in the login page to visualize the user first name.
        public string UserFirstName
        {
            get
            {
                string firstName = string.Empty;

                if (User != null && User.HasClaim(x => x.Type == "first_name"))
                {
                    firstName = User.Claims.FirstOrDefault(x => x.Type == "first_name")?.Value ?? firstName;

                }

                return firstName;


            }


        }

    }
}
