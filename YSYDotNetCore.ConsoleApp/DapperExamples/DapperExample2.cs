using Dapper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using YSYDotNetCore.ConsoleApp.Models;
using YSYDotNetCore.Services;

namespace YSYDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample2
    {

        

        private readonly DapperServices _dapperservice=new DapperServices(new SqlConnectionStringBuilder
        {
            DataSource = "NYEINCHANMOE\\SQL2022",
                InitialCatalog = "YSYDotNetCore",
                UserID = "sa",
                Password = "201328"

        });

        public void run()
        {
             //read();
             // Edit(81);
            //create("Test Title", "Test Author", "Test Content");
           //update(90,"Test Title", "Test Author", "Test Content");
            Delete(85);
        }

        private void read()
        {
                string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog]";
            var lst = _dapperservice.Query<BlogDataModel>(query);

            foreach (BlogDataModel item in lst)
                {
                    Console.WriteLine(item.Blog_Id);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_content);
                    Console.WriteLine(item.Blog_content);

                }

        }

        private void Edit(int id)
        { 

            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";


            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
            };

            BlogDataModel? item = _dapperservice.Query<BlogDataModel>(query,param:blog).FirstOrDefault();


            if (item is null)
            {
                Console.WriteLine("No data found");

                return;
            }
            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_content);

        }

        private void create(string title, string author, string content)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_content)";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_content = content
            };

           
            int result = _dapperservice.Execute(query, blog);


            string message = result > 0 ? "Saving Successful.." : "Saving Fail";
            Console.WriteLine(message);
        }

        private void update(int id, string title, string author, string content)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
            SET [Blog_Title] = @Blog_Title,
           [Blog_Author] = @Blog_Author, 
           [Blog_content] = @Blog_content
           WHERE Blog_Id= @Blog_Id";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_content = content
            };
            int result = _dapperservice.Execute(query, blog);


            string message = result > 0 ? "Update Successful.." : "Update Fail";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {


            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE Blog_Id=@Blog_Id";

          
            int result = _dapperservice.Execute(query, new BlogDataModel { Blog_Id = id });

            string message = result > 0 ? "Delete Successful.." : "Delete Fail";
            Console.WriteLine(message);
        }


    }
}

