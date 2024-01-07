using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSYDotNetCore.ConsoleApp.Models;

namespace YSYDotNetCore.ConsoleApp.EFCoreExamples
{
    public class AppDBContext:DbContext
    {
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "NYEINCHANMOE\\SQL2022",
                InitialCatalog = "YSYDotNetCore",
                UserID = "sa",
                Password = "201328",
                TrustServerCertificate = true,
            };

            optionsBuilder.UseSqlServer(_sqlConnectionStringBuilder.ConnectionString);
        }

        public DbSet<BlogDataModel> Blogs { get; set; }

     }
}
