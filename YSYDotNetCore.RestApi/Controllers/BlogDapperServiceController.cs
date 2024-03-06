using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using YSYDotNetCore.RestApi.Models;
using YSYDotNetCore.Services;

namespace YSYDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperServiceController : ControllerBase
    {
        private readonly DapperServices _dapperservice = new DapperServices(new SqlConnectionStringBuilder
        {
            DataSource = "NYEINCHANMOE\\SQL2022",
            InitialCatalog = "YSYDotNetCore",
            UserID = "sa",
            Password = "201328"

        });

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog]";
            
            List<BlogDataModel> lst = _dapperservice.Query<BlogDataModel>(query).ToList();

            return Ok(lst);

        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";
             BlogDataModel? item = _dapperservice.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
            if (item is null)
            {
                return NotFound("No data found");


            }
            return Ok(item);

        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogDataModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_content)"
            ;

            int result = _dapperservice.Execute(query, blog);

            var message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogDataModel blog)
        {
           

            #region Get By Id
            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";
            BlogDataModel? item = _dapperservice.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("No data Found");
            }

            #endregion


            #region check requied field


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

            #endregion



            string queryupdate = @"UPDATE [dbo].[Tbl_Blog]
            SET [Blog_Title] = @Blog_Title,
           [Blog_Author] = @Blog_Author, 
           [Blog_content] = @Blog_content
           WHERE Blog_Id= @Blog_Id";

            blog.Blog_Id = id;
            int result = _dapperservice.Execute(queryupdate, blog);
            string message = result > 0 ? "Update Successful ." : "Update Failed.";

            return Ok(message);


        }

        [HttpPatch("{id}")]

        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
                        #region Get Id

            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";
            BlogDataModel? item = _dapperservice.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("No data Found");
            }

            #endregion

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += @"[Blog_Title] = @Blog_Title, ";
            }

            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                conditions += @"[Blog_Author] = @Blog_Author, ";
            }

            if (!string.IsNullOrEmpty(blog.Blog_content))
            {
                conditions += @"[Blog_content] = @Blog_content, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Request");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string queryupdate = $@"UPDATE [dbo].[Tbl_Blog]
            SET{conditions}
           WHERE Blog_Id= @Blog_Id";

            blog.Blog_Id = id;

            #region update
            int result = _dapperservice.Execute(queryupdate, blog);
            string message = result > 0 ? "Update Successful ." : "Update Failed.";

            #endregion

            return Ok(message);


        }

    }
}
