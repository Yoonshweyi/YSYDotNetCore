using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSYDotNetCore.Services;
using System.Reflection.Metadata;
using YSYDotNetCore.ConsoleApp.Models;

namespace YSYDotNetCore.ConsoleApp.AdoDotNetExamples
{
    internal class AdoDotNetExample2
    {
        private readonly AdoDotNetService _adoDotNetService=new AdoDotNetService(new SqlConnectionStringBuilder()
        {
            DataSource = "NYEINCHANMOE\\SQL2022",
            InitialCatalog = "YSYDotNetCore",
            UserID = "sa",
            Password = "201328"
        });
        public void run()
        {
           Read();
           //Create("Test Title","Test Author","Test Content");
           //Edit(89);
           //(12);
           //Update(89,"Test Title", "Test Author", "Test Content");
           //Delete(89);
        }

        private void Read()
        {

            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog]";

            var lst = _adoDotNetService.Query<BlogDataModel>(query);
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_content);
                Console.WriteLine(item.Blog_content);

            }
            // var dt= _adoDotNetService.Query(query);

            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine("Id=>" + dr["Blog_Id"]);
            //    Console.WriteLine("Title=>" + dr["Blog_Title"]);
            //    Console.WriteLine("Author=>" + dr["Blog_Author"]);
            //    Console.WriteLine("Content=>" + dr["Blog_Content"]);
            //    Console.WriteLine("------------------");

            //}


        }

        private void Edit(int id)
        {

            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";

            List<SqlParameter> lstparameters = new List<SqlParameter>()
            {
                new("@Blog_Id", id)
            };

            var dt = _adoDotNetService.Query(query,sqlParameters:lstparameters.ToArray());

            
            if(dt.Rows.Count == 0) {
                Console.WriteLine("No Data Found");
                return;
            }

            DataRow dr= dt.Rows[0];
                Console.WriteLine("Id=>" + dr["Blog_Id"]);
                Console.WriteLine("Title=>" + dr["Blog_Title"]);
                Console.WriteLine("Author=>" + dr["Blog_Author"]);
                Console.WriteLine("Content=>" + dr["Blog_Content"]);
                Console.WriteLine("------------------");

        }

        private void Create(string title,string author,string content) {

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
                new("@Blog_Title", title),
                new("@Blog_Author", author),
                new("@Blog_content", content)
            };

            var result = _adoDotNetService.Execute(query, sqlParameters: lstparameters.ToArray());

            string message = result > 0 ? "Saving Successful.." : "Saving Fail";
            Console.WriteLine(message);

           

        }

        private void Update(int id,string title, string author, string content)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
            SET [Blog_Title] = @Blog_Title,
           [Blog_Author] = @Blog_Author, 
           [Blog_content] = @Blog_content
           WHERE Blog_Id= @Blog_Id";

            List<SqlParameter> lstparameters = new List<SqlParameter>()
            {
                new("@Blog_Id", id),
                new("@Blog_Title", title),
                new("@Blog_Author", author),
                new("@Blog_content", content)
            };

            var result = _adoDotNetService.Execute(query, sqlParameters: lstparameters.ToArray());
            string message = result > 0 ? "Update Successful.." : "Update Fail";
            Console.WriteLine(message);



        }

        private void Delete(int id)
        {

           
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE Blog_Id=@Blog_Id";
            List<SqlParameter> lstparameters = new List<SqlParameter>()
            {
                new("@Blog_Id", id),
               
            };

            var result = _adoDotNetService.Execute(query, sqlParameters: lstparameters.ToArray());
            string message = result > 0 ? "Delete Successful.." : "Delete Fail";
            Console.WriteLine(message);



        }
    }
}
