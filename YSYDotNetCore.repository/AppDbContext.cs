using Microsoft.EntityFrameworkCore;
using YSYDotNetCore.repository.Models;


namespace YSYDotNetCore.repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
    }
}
