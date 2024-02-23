using Microsoft.EntityFrameworkCore;
using YSYDotNetCore.MinimalApi.Models;


namespace YSYDotNetCore.MinimalApi
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<BlogDataModel> Blogs { get; set; }

       
    }
}
