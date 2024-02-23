using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using YSYDotNetCore.MinimalApi;
using YSYDotNetCore.MinimalApi.Models;

namespace YSYDotNetCore.MinimalAPi.Features.Blog
{
    public static  class BlogServices
    {
        public static IEndpointRouteBuilder UseBlogService(this IEndpointRouteBuilder app) {
            app.MapGet("/api/blog/{pageNo}/{pageSize}", async ([FromServicesAttribute] AppDbContext _dbContext, int pageNo, int pageSize) =>
            {
                return await _dbContext.Blogs
                 .AsNoTracking()
                 .OrderByDescending(x => x.Blog_Id)
                 .Skip((pageNo - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
            })
            .WithName("GetBlogs")
            .WithOpenApi();

            app.MapGet("/api/blog/{id}", async ([FromServicesAttribute] AppDbContext _dbContext, int id) =>
            {
                var item = await _dbContext.Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Blog_Id == id);
                if (item is null)
                {
                    return Results.NotFound("No data Found.");
                }
                return Results.Ok(item);
            })
            .WithName("GetBlog")
            .WithOpenApi();

            app.MapPost("/api/blog", async ([FromServicesAttribute] AppDbContext _dbContext,[FromBody] BlogDataModel blog) =>
            {
                await _dbContext.Blogs.AddAsync(blog);
                var result = await _dbContext.SaveChangesAsync();
                var message = result > 0 ? "Saving Successful" : "Saving Failed";
                return Results.Ok(message);

            })
            .WithName("CreateBlog")
            .WithOpenApi();

            app.MapPut("/api/blog/{id}", async ([FromServicesAttribute] AppDbContext _dbContext, int id, [FromBody]BlogDataModel blog) =>
            {

                var item =await _dbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
                if (item is null)
                {
                    return Results.NotFound("No data Found.");
                }

                if (string.IsNullOrEmpty(blog.Blog_Title))
                {
                   return Results.BadRequest("Blog Title is required");
                }
                if (string.IsNullOrEmpty(blog.Blog_Author))
                {
                    return Results.BadRequest("Blog Author is required");
                }
                if (string.IsNullOrEmpty(blog.Blog_content))
                {
                    return Results.BadRequest("Blog Content is required");
                }



                item.Blog_Title = blog.Blog_Title;
                item.Blog_Author = blog.Blog_Author;
                item.Blog_content = blog.Blog_content;

                int result = await _dbContext.SaveChangesAsync();
                string message = result > 0 ? "Update Successful ." : "Update Failed.";

                return Results.Ok(message);
            })
           .WithName("UpdateBlog")
           .WithOpenApi();

            app.MapPatch("/api/blog/{id}", async ([FromServicesAttribute] AppDbContext _dbContext, int id, [FromBody]BlogDataModel blog) =>
            {
                var item =await _dbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
                if (item is null)
                {
                    return Results.NotFound("No Data Dound");
                }
                if (!string.IsNullOrEmpty(blog.Blog_Title))
                {
                    item.Blog_Title = blog.Blog_Title;
                }
                if (!string.IsNullOrEmpty(blog.Blog_Author))
                {
                    item.Blog_Author = blog.Blog_Author;
                }
                if (!string.IsNullOrEmpty(blog.Blog_content))
                {
                    item.Blog_content = blog.Blog_content;
                }
                int result = await _dbContext.SaveChangesAsync();
                string message = result > 0 ? "Update Successful ." : "Update Failed.";
                return Results.Ok(message);
            })
           .WithName("PatchBlog")
           .WithOpenApi();


            app.MapDelete("/api/blog/{id}", async ([FromServicesAttribute] AppDbContext _dbContext, int id) =>
            {
                var item = await _dbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
                if (item is null)
                {
                    return Results.NotFound();
                }
                 _dbContext.Blogs.Remove(item);
                int result = await _dbContext.SaveChangesAsync();
                string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
                return Results.Ok(message);
            })
            .WithName("DeleteBlog")
            .WithOpenApi();


            return app;
        }
    }
}
