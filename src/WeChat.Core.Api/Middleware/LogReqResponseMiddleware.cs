using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WeChat.Core.Api.Controllers;
using WeChat.Core.Api.Log;

namespace WeChat.Core.Api.Middleware
{
    public class LogReqResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerHelper _logger;

        public LogReqResponseMiddleware(RequestDelegate next, ILoggerHelper logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var request = httpContext.Request;
            request.EnableBuffering();
            Console.WriteLine(request.ContentType);
            var bodyAsText = string.Empty;

            //把请求body流转换成字符串
            if (request.Method == "POST")
            {
                if (request.ContentType != null && request.ContentType.Contains("multipart/form-data"))
                {
                    if (request.Form.Files.Count > 0)
                    {
                        var file = request.Form.Files.FirstOrDefault();
                        if (file != null) bodyAsText = $"{file.ContentDisposition}-{file.Length}";
                        Console.WriteLine(bodyAsText);
                    }
                }
                else
                {
                    bodyAsText = await new StreamReader(request.Body).ReadToEndAsync();//记录请求信息
                }
            }
            else
            {

                bodyAsText = await new StreamReader(request.Body).ReadToEndAsync(); //记录请求信息
            }

            var requestStr = $"{request.Scheme} {request.Host} {request.Path} {request.Method} {request.QueryString} {bodyAsText}";
            _logger.Info(typeof(LogReqResponseMiddleware), "Request:" + requestStr);
            request.Body.Seek(0, SeekOrigin.Begin);

            var originalBodyStream = httpContext.Response.Body;
            await using var responseBody = new MemoryStream();
            httpContext.Response.Body = responseBody;
            await _next(httpContext);

            var response = httpContext.Response;
            response.Body.Seek(0, SeekOrigin.Begin);
            string text=await new StreamReader(response.Body).ReadToEndAsync();
            //从新设置偏移量0
            response.Body.Seek(0, SeekOrigin.Begin);

            //记录返回值
            var responsestr = $"{response.StatusCode}: {text}";
            _logger.Info(typeof(LogReqResponseMiddleware), "Response:" + responsestr);

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LogReqResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogReqResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogReqResponseMiddleware>();
        }
    }
}
