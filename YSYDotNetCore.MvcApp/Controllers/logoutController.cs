using Microsoft.AspNetCore.Mvc;

namespace YSYDotNetCore.MvcApp.Controllers
{
    public class logoutController : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            Response.Cookies.Delete("Auth");
            return Redirect("/");
        }
    }
}
