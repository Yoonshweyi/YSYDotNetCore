using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace YSYDotNetCore.RestApi.Controllers
{
    //https://localhost:3000/api/Blog
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDBContext _dbContext = new AppDBContext();
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _dbContext.Blogs.ToList();
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateBlogs()
        {
            return Ok("post");
        }

        [HttpPut]
        public IActionResult UpdateBlogs()
        {
            return Ok("put");
        }

        [HttpPatch]
        public IActionResult PatchBlogs()
        {
            return Ok("patch");
        }

        [HttpDelete]
        public IActionResult DeleteBlogs()
        {
            return Ok("delete");
        }
    }
}
