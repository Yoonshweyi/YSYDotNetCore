using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RestSharp;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using YSYDotNetCore.ConsoleApp.RefitExamples;
using YSYDotNetCore.MvcApp;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/MyAppfile.log", rollingInterval: RollingInterval.Hour)
    .WriteTo
    .MSSqlServer(
    connectionString: "Server=NYEINCHANMOE\\SQL2022;Database=YSYDotNetCore;User ID=sa;Password=201328;TrustServerCertificate = true;",
    sinkOptions: new MSSqlServerSinkOptions { TableName = "Tbl_Logfile", AutoCreateSqlTable = true })
    .CreateLogger();

try { 
Log.Information("Starting web application");
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    string connectionString = builder.Configuration.GetConnectionString("DbConnection");
    opt.UseSqlServer(connectionString);
});

//builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped(n =>
{
    HttpClient httpclient = new HttpClient()
    {
        BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrl").Value!)
    };
    return httpclient;
});

builder.Services.AddScoped(n =>
{
    RestClient restClient = new RestClient(builder.Configuration.GetSection("ApiUrl").Value!);
    return restClient;
});

builder.Services
     .AddRefitClient<IBlogApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrl").Value!));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
