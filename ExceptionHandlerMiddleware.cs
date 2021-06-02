using System;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using SimpleHTTPClient;

namespace PCRArenaCrawler
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try {
                await next(context);
            } catch (Exception e) {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {          
            var response = context.Response;
            response.ContentType = "application/json; charset=utf-8";
            response.StatusCode = 200;
            var apiResult = new APIResult<string>()
            {
                Code = 500,
                Message = e.Message,
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(apiResult));
        }
    }
}