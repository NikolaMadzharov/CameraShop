using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechRentingSystem.Infrastructure;

namespace TechRentingSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    [Area("Admin")]
    public class BaseController : Controller
    {
        
    }
}
