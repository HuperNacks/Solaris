using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "RequireMaster")]
        public IActionResult Master()
        {
            return View();
        }

        [Authorize(Roles  = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
