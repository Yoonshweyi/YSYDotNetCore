
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using YSYDotNetCore.ConsoleApp.AdoDotNetExamples;
using YSYDotNetCore.ConsoleApp.DapperExamples;
using YSYDotNetCore.ConsoleApp.EFCoreExamples;

Console.WriteLine("Hello, World!");
//SqlConnectionStringBuilder sqlConnectionStringBuilder=new SqlConnectionStringBuilder();
//sqlConnectionStringBuilder.DataSource = "NYEINCHANMOE\\SQL2022";
//sqlConnectionStringBuilder.InitialCatalog = "YSYDotNetCore";
//sqlConnectionStringBuilder.UserID = "sa";
//sqlConnectionStringBuilder.Password = "201328";

/*SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
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
   
}*/

//AdoDotNetExample adoDotNetExample= new AdoDotNetExample();
//adoDotNetExample.run();

//DapperExample dapperExample= new DapperExample();
//dapperExample.run();

EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.run();
Console.ReadKey();
