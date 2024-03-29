﻿
using AppKursProject.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace AppKursProject.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next) => _next = next;
        public System.Threading.Tasks.Task Invoke(HttpContext context, IServiceProvider serviceProvider, ApplicationDbContext dbContext)
        {
            if (!(context.Session.Keys.Contains("starting")))
            {
                
                RolesInitializer.Initialize(context).Wait();
                context.Session.SetString("starting", "Yes");
            }

            return _next.Invoke(context);
        }
    }

    public static class DbInitializerExtensions
    {
        public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializerMiddleware>();
        }

    }
}
