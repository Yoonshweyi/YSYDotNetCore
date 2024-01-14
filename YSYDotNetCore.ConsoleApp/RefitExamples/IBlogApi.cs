using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSYDotNetCore.ConsoleApp.Models;

namespace YSYDotNetCore.ConsoleApp.RefitExamples
{
    public interface IBlogApi
    {
        [Get("/api/Blog")]
        Task<List<BlogDataModel>> GetBlogs( );

        [Get("/api/Blog/{id}")]
        Task<BlogDataModel> GetBlog(int id);

        [Post("/api/Blog")]
        Task<string> CreateBlog(BlogDataModel blog);

        [Put("/api/Blog/{id}")]
        Task<string> UpdateBlog(int id,BlogDataModel blog);

        [Delete("/api/Blog/{id}")]
        Task<string> DeleteBlog(int id);


    }
}
