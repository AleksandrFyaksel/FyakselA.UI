﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog; 
using System.Threading.Tasks;

namespace FyakselA.UI.Middleware
{
    
    public class FileLogger
    {
        private readonly RequestDelegate _next;

        public FileLogger(RequestDelegate next)
        {
            _next = next; 
        }

        public async Task Invoke(HttpContext httpContext)
        {
            
            await _next(httpContext);

            
            var code = httpContext.Response.StatusCode;

            
            if (code / 100 != 2)
            {
                
                Log.Logger.Information($"---> Request {httpContext.Request.Path} returns {code}");
            }
        }
    }

   
    public static class FileLoggerExtensions
    {
        public static IApplicationBuilder UseFileLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FileLogger>(); 
        }
    }
}
