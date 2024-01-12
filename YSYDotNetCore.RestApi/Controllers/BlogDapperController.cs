using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using YSYDotNetCore.RestApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace YSYDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "NYEINCHANMOE\\SQL2022",
            InitialCatalog = "YSYDotNetCore",
            UserID = "sa",
            Password = "201328",
            TrustServerCertificate = true,
        };

        //select
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog]";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();

            return Ok(lst);

        }


        //edit

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
            if (item is null)
            {
                return NotFound("No data found");

                
            }
            return Ok(item);

        }


        //create
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
           ,@Blog_content)";


            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            var message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }



        //delete
        [HttpDelete("{id}")]
        public IActionResult  Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE Blog_Id=@Blog_Id";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, new BlogDataModel { Blog_Id = id });

            string message = result > 0 ? "Delete Successful.." : "Delete Fail";
            return Ok(message);

        }


        //put(update)
        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            #region Get By Id
            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

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
            int result = db.Execute(queryupdate, blog);
            string message = result > 0 ? "Update Successful ." : "Update Failed.";

            return Ok(message);


        }
        


        //patch
        [HttpPatch("{id}")]

        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            #region Get Id

            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

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

            if(conditions.Length == 0)
            {
                return BadRequest("Invalid Request");
            }

            conditions= conditions.Substring(0, conditions.Length - 2);
           
            string queryupdate = $@"UPDATE [dbo].[Tbl_Blog]
            SET{conditions}
           WHERE Blog_Id= @Blog_Id";

            blog.Blog_Id = id;

            #region update
            int result = db.Execute(queryupdate, blog);
            string message = result > 0 ? "Update Successful ." : "Update Failed.";

            #endregion

            return Ok(message);

           
        }


    }
}
