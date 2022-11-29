namespace TechRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    [Authorize]
    public class BaseController : Controller
    {
        
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
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                ViewBag.UserFirstName = UserFirstName;
               
            }

            base.OnActionExecuted(context);
        }
    }
}
