using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Middleware.Middleware
{

    // REMEMBER: Add `services.AddTransient<NameMiddleware>();` to Startup.ConfigureServices() method
    public class BlockIpMiddleware : IMiddleware
    {
        // IMiddleware is activated per request, 
        // so scoped services can be injected into the middleware's constructor.
        public BlockIpMiddleware()
        {
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            if (!string.IsNullOrEmpty(ip) && ip == "192.168.9.9")
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied from your region.");
                return;
            }
            await next(context);
        }
    }
}
