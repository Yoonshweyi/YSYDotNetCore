using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSYDotNetCore.ConsoleApp.AdoDotNetExamples
{
    internal class AdoDotNetExample
    {
        public void run()
        {
            //Read();
           // Create("Test Title","Test Author","Test Content");
           //Edit(1);
             //(12);
            //Update(1,"Test Title", "Test Author", "Test Content");
            Delete(13);
        }

        private void Read()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "NYEINCHANMOE\\SQL2022",
                InitialCatalog = "YSYDotNetCore",
                UserID = "sa",
                Password = "201328"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            Console.WriteLine("Connection opening...");
            connection.Open();
            Console.WriteLine("Connection opened.");

            Console.WriteLine("Connection Closing...");
            connection.Close();
            Console.WriteLine("Connection Closed");

            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog]";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Id=>" + dr["Blog_Id"]);
                Console.WriteLine("Title=>" + dr["Blog_Title"]);
                Console.WriteLine("Author=>" + dr["Blog_Author"]);
                Console.WriteLine("Content=>" + dr["Blog_Content"]);
                Console.WriteLine("------------------");

            }


        }

        private void Edit(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "NYEINCHANMOE\\SQL2022",
                InitialCatalog = "YSYDotNetCore",
                UserID = "sa",
                Password = "201328"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
           
            connection.Open();
           
           
           

            string query = @"SELECT [Blog_Id],
                             [Blog_Title],
                             [Blog_Author],
                             [Blog_content]
                             FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
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

            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "NYEINCHANMOE\\SQL2022",
                InitialCatalog = "YSYDotNetCore",
                UserID = "sa",
                Password = "201328"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
           
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_content)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_content", content);
            int result= command.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Saving Successful.." : "Saving Fail";
            Console.WriteLine(message);

           

        }

        private void Update(int id,string title, string author, string content)
        {

            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "NYEINCHANMOE\\SQL2022",
                InitialCatalog = "YSYDotNetCore",
                UserID = "sa",
                Password = "201328"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
            SET [Blog_Title] = @Blog_Title,
           [Blog_Author] = @Blog_Author, 
           [Blog_content] = @Blog_content
           WHERE Blog_Id= @Blog_Id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_content", content);
            int result = command.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Update Successful.." : "Update Fail";
            Console.WriteLine(message);



        }

        private void Delete(int id)
        {

            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "NYEINCHANMOE\\SQL2022",
                InitialCatalog = "YSYDotNetCore",
                UserID = "sa",
                Password = "201328"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE Blog_Id=@Blog_Id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            int result = command.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Delete Successful.." : "Delete Fail";
            Console.WriteLine(message);



        }
    }
}
