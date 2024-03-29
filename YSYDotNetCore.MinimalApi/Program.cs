using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Data.SqlClient;
using YSYDotNetCore.MinimalApi;
using YSYDotNetCore.MinimalApi.Models;
using YSYDotNetCore.MinimalAPi.Features.AdoDotNetBlog;
using YSYDotNetCore.MinimalAPi.Features.Blog;
using YSYDotNetCore.Services;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Hour)
            .CreateLogger();
try
{

    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
    }, ServiceLifetime.Transient, ServiceLifetime.Transient);



    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped(n => new AdoDotNetService(new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DbConnection"))));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    // app.UseBlogService();
    app.UseAdoDotNetBlogService();
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









