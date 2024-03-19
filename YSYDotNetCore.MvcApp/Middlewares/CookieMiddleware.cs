using Microsoft.EntityFrameworkCore;
using System.Globalization;
using YSYDotNetCore.MvcApp.Controllers;

namespace YSYDotNetCore.MvcApp.Middlewares
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppDbContext _appDbContext;

        public CookieMiddleware(RequestDelegate next,AppDbContext db)
        {
            _next = next;
            _appDbContext = db;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/" || context.Request.Path.ToString().ToLower()=="/home/index")
                goto result;

            var auth = context.Request.Cookies["Auth"];
            if (string.IsNullOrWhiteSpace(auth))
            {
                context.Response.Redirect("/Home");
                return;
            }

            var isExist = await _appDbContext.ValidateTokenAsync(auth);
            if (!isExist)
            {
                context.Response.Redirect("/");
            }
            result:

            // Call the next delegate/middleware in the pipeline.
            await _next(context);

        }
    }

    

    public static class CookieMiddlewareExtensions
    {
        public static IApplicationBuilder UseCookieMiddleware(this IApplicationBuilder app)
        {
          return  app.UseMiddleware<CookieMiddleware>();
        }
    }
}
