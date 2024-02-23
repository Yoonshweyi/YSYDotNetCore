using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YSYDotNetCore.MinimalApi;
using YSYDotNetCore.MinimalApi.Models;
using YSYDotNetCore.MinimalAPi.Features.Blog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
},ServiceLifetime.Transient,ServiceLifetime.Transient);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseBlogService();







app.Run();

