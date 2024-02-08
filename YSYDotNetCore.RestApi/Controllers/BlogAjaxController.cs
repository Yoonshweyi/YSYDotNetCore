using Microsoft.AspNetCore.Mvc;

namespace YSYDotNetCore.RestApi.Controllers
{
    public class BlogAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
