using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using YSYDotNetCore.MinimalApi;
using YSYDotNetCore.MinimalApi.Models;
using YSYDotNetCore.Services;

namespace YSYDotNetCore.MinimalAPi.Features.AdoDotNetBlog
{
    public static  class AdoDotNetBlogServices
    {
        public static IEndpointRouteBuilder UseAdoDotNetBlogService(this IEndpointRouteBuilder app) {
            app.MapGet("/api/blog", async ([FromServices] AdoDotNetService _adoDotNetService) =>
            {

                string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog]";

                var lst = _adoDotNetService.Query<BlogDataModel>(query);
                return Results.Ok(lst);
               
            })
            .WithName("GetBlogs")
            .WithOpenApi();

            app.MapGet("/api/blog/{id}", async ([FromServicesAttribute] AdoDotNetService _adoDotNetService, int id) =>
            {
            //    string query = @"SELECT [Blog_Id],
            //                 [Blog_Title],
            //                 [Blog_Author],
            //                 [Blog_content]
            //                 FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";

            //    List<SqlParameter> lstparameters = new List<SqlParameter>()
            //{
            //    new("@Blog_Id", id)
            //};

            //    var dt = _adoDotNetService.Query(query, sqlParameters: lstparameters.ToArray());


            //    if (dt.Rows.Count == 0)
            //    {
            //        Console.WriteLine("No Data Found");
            //        return;
            //    }


                string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";

                List<SqlParameter> lstparameters = new List<SqlParameter>()
            {
                new("@Blog_Id", id)
            };
                var item = _adoDotNetService.Query<BlogDataModel>(query, sqlParameters: lstparameters.ToArray());
                
                if (item == null || !item.Any())
                {
                    return Results.NotFound("No data Found.");
                }

                return Results.Ok(item);
            })
            .WithName("GetBlog")
            .WithOpenApi();

            app.MapPost("/api/blog", async ([FromServicesAttribute] AdoDotNetService _adoDotNetService, [FromBody] BlogDataModel blog) =>
            {
                //await _dbContext.Blogs.AddAsync(blog);
                //var result = await _dbContext.SaveChangesAsync();
                //var message = result > 0 ? "Saving Successful" : "Saving Failed";
                string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_content)";


                List<SqlParameter> lstparameters = new List<SqlParameter>()
            {
                new("@Blog_Title", blog.Blog_Title),
                new("@Blog_Author", blog.Blog_Author),
                new("@Blog_content", blog.Blog_content)
            };
                var result = _adoDotNetService.Execute(query, sqlParameters: lstparameters.ToArray());
                

                var message = result > 0 ? "Saving Successful" : "Saving Failed";

                return Results.Ok(message);

            })
            .WithName("CreateBlog")
            .WithOpenApi();

            app.MapPut("/api/blog/{id}", async ([FromServicesAttribute] AdoDotNetService _adoDotNetService, int id, [FromBody] BlogDataModel blog) =>
            {

                string query = @"UPDATE [dbo].[Tbl_Blog]
            SET [Blog_Title] = @Blog_Title,
           [Blog_Author] = @Blog_Author, 
           [Blog_content] = @Blog_content
           WHERE Blog_Id= @Blog_Id";

                List<SqlParameter> lstparameters = new List<SqlParameter>()
            {
                new("@Blog_Id", id),
                new("@Blog_Title",blog.Blog_Title ),
                new("@Blog_Author", blog.Blog_Author),
                new("@Blog_content", blog.Blog_content)
            };
                var result = _adoDotNetService.Execute(query, sqlParameters: lstparameters.ToArray());
                if (result == 0)
                {
                    return Results.NotFound("No data Found.");
                }


                string message = result > 0 ? "Update Successful ." : "Update Failed.";

                return Results.Ok(message);
            })
           .WithName("UpdateBlog")
           .WithOpenApi();

            app.MapDelete("/api/blog/{id}", async ([FromServicesAttribute] AdoDotNetService _adoDotNetService, int id) =>
            {

                string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE Blog_Id=@Blog_Id";
                List<SqlParameter> lstparameters = new List<SqlParameter>()
            {
                new("@Blog_Id", id),

            };

                var result = _adoDotNetService.Execute(query, sqlParameters: lstparameters.ToArray());
                if (result == 0)
                {
                    return Results.NotFound("No data Found.");
                }

                string message = result > 0 ? "Delete Successful.." : "Delete Fail";
                return Results.Ok(message);
            })
            .WithName("DeleteBlog")
            .WithOpenApi();


            return app;
        }
    }
}
