using Microsoft.EntityFrameworkCore;
using YSYDotNetCore.MvcApp.Models;

namespace YSYDotNetCore.MvcApp
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
