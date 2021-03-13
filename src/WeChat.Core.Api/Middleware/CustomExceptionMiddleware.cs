using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WeChat.Core.Api.Log;

namespace WeChat.Core.Api.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerHelper _logger;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerHelper logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex); // 日志记录
                await HandleExceptionAsync(httpContext, ex.Message);
            }
            finally
            {
                var statusCode = httpContext.Response.StatusCode;
                var msg = "";
                switch (statusCode)
                {
                    case 401:
                        msg = "未授权";
                        break;
                    case 403:
                        msg = "拒绝访问";
                        break;
                    case 404:
                        msg = "未找到服务";
                        break;
                    case 405:
                        msg = "405 Method Not Allowed";
                        break;
                    case 502:
                        msg = "请求错误";
                        break;
                }
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    await HandleExceptionAsync(httpContext, msg);
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, string msg)
        {
            ErrorModel error = new ErrorModel
            {
                code = httpContext.Response.StatusCode,
                msg = msg
            };
            var result = JsonConvert.SerializeObject(error);
            httpContext.Response.ContentType = "application/json;charset=utf-8";
            await httpContext.Response.WriteAsync(result).ConfigureAwait(false);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }

    /// <summary>
    /// 自定义返回消息实体类
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; } = 500;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 错误详情
        /// </summary>
        public string detail { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}