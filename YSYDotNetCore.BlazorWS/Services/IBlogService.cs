using System.Numerics;
using YSYDotNetCore.Models;

namespace YSYDotNetCore.BlazorWS.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogDataModel>> GetBlogListAsync();

        Task<BlogDataModel> GetBlogByIdAsync(int id);
        Task UpdateBlogAsync(BlogDataModel blog,int id);
        Task DeleteBlogAsync(int id);
        Task CreateBlogAsync(BlogDataModel blog);
    }
}
