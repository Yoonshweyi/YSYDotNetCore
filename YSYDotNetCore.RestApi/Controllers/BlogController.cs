using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YSYDotNetCore.RestApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YSYDotNetCore.RestApi.Controllers
{
    
    //https://localhost:3000/api/Blog
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDBContext _dbContext = new AppDBContext();
        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into BlogController");
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _dbContext.Blogs.ToList();
            _logger.LogInformation("Hello, this is the index!");
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if(item is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(item);
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetBlogs(int pageNo,int pageSize)
        {
           var lst= _dbContext.Blogs
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            var rowcount= _dbContext.Blogs.Count();
            var pagecount=rowcount/pageSize;
            if (rowcount % pageSize > 0)
            {
                pagecount++;
            }
            return Ok(new
            {
               EndOfPage = pageNo >= pagecount,
                PageCount = pagecount,
                PageNo = pageNo,
                PageSize = pageSize,
                Data = lst
            } );



        }


        [HttpPost]
        public IActionResult CreateBlogs(BlogDataModel blog)
        {
            _logger.LogInformation("Create  blog Model => " + JsonConvert.SerializeObject(blog));
            _dbContext.Blogs.Add(blog);
            var result=_dbContext.SaveChanges();
            var message = result > 0 ? "Saving Successful" : "Saving Failed";
            _logger.LogInformation(message);

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogDataModel blog)
        {
            _logger.LogInformation("Update  blog Model => " + JsonConvert.SerializeObject(blog));
            var item= _dbContext.Blogs.FirstOrDefault(x=>x.Blog_Id == id);
            if(item is null)
            {
                return NotFound("No data Found");
            }

            if (string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog Title is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Author))
            {
                return BadRequest("Blog Author is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_content))
            {
                return BadRequest("Blog Content is required");
            }



            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_content = blog.Blog_content;

            int result= _dbContext.SaveChanges();
            string message = result > 0 ? "Update Successful ." : "Update Failed.";
            _logger.LogInformation(message);


            return Ok(message);
          
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogDataModel blog)
        {
            _logger.LogInformation("Patch  blog Model => " + JsonConvert.SerializeObject(blog));
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if(item is null)
            {
                return NotFound("No Data Dound");
            }
            
            if(!string.IsNullOrEmpty(blog.Blog_Title))
            {
                item.Blog_Title=blog.Blog_Title;
            }

            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                item.Blog_Author = blog.Blog_Author;
            }
            if (!string.IsNullOrEmpty(blog.Blog_content))
            {
                item.Blog_content = blog.Blog_content;
            }

            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Update Successful ." : "Update Failed.";
            _logger.LogInformation(message);


            return Ok(message);


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            _logger.LogInformation("Delete blog Model => " + JsonConvert.SerializeObject(id));
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound();
            }
            _dbContext.Blogs.Remove(item);
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            _logger.LogInformation(message);

            return Ok(message);
        }
    }
}
