using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _dbContext.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetBlogs(int pageNo,int pageSize)
        {
           //pageNo=1[1-10]
           //pageNo=2[11-20]
           //endRowno=pageno*pagesize;(1*10)
           //startrowno=endrowno-pagesize+1;(10-10)+1
           //567=5670-10=5660+1=5661-5670
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
               EndOfPage = pageNo >=pagecount,
                PageCount = pagecount,
                PageNo = pageNo,
                PageSize = pageSize,
                Data = lst
            } );



        }


        [HttpPost]
        public IActionResult CreateBlogs(BlogDataModel blog)
        {
            _dbContext.Blogs.Add(blog);
            var result=_dbContext.SaveChanges();
            var message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
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
