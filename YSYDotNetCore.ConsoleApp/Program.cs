﻿
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using YSYDotNetCore.ConsoleApp.AdoDotNetExamples;
using YSYDotNetCore.ConsoleApp.DapperExamples;
using YSYDotNetCore.ConsoleApp.EFCoreExamples;
using YSYDotNetCore.ConsoleApp.HttpClientExamples;
using YSYDotNetCore.ConsoleApp.RefitExamples;
using YSYDotNetCore.ConsoleApp.RestClientExamples;

//Log.Logger = new LoggerConfiguration()
//            .MinimumLevel.Debug()
//            .WriteTo.Console()
//            .WriteTo.File("logs/myapp.log", rollingInterval: RollingInterval.Hour)
//            .WriteTo
//            .MSSqlServer(
//            connectionString: "Server=NYEINCHANMOE\\SQL2022;Database=YSYDotNetCore;User ID=sa;Password=201328;TrustServerCertificate = true;",
//            sinkOptions: new MSSqlServerSinkOptions { TableName = "Tbl_Log",AutoCreateSqlTable=true })
//            .CreateLogger();



//Console.WriteLine("Hello, World!");
//Log.Information("Hello, world!");

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

//AdoDotNetExample2 adoDotNetExample2 = new AdoDotNetExample2();
//adoDotNetExample2.run();

DapperExample2 dapperExample2= new DapperExample2();
dapperExample2.run();

//DapperExample dapperExample= new DapperExample();
//dapperExample.run();

//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.run();

//HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.run();

//RestClientExample restClientExample= new RestClientExample();
//await restClientExample.run();

//Console.WriteLine("Please wait for api...");
//Console.ReadKey();

//int a = 10, b = 0;
//try
//{
//    Log.Debug("Dividing {A} by {B}", a, b);
//    Console.WriteLine(a / b);
//}
//catch (Exception ex)
//{
//    Log.Error(ex, "Something went wrong");
//}
//finally
//{
//    await Log.CloseAndFlushAsync();
//}

//RefitExample refitExample= new RefitExample();
//await refitExample.Run();

