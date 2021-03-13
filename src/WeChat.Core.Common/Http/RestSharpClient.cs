using System;
using System.Drawing;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace WeChat.Core.Common.Http
{
    public class RestSharpClient:IRestSharp
    {
        /// <summary>
        /// 请求连接
        /// </summary>
        private RestClient client;
        
        private string BaseUrl { get; set; }

        public string DefaultDateParameterFormat { get; set; }

        public IAuthenticator DefaultAuthenticator { get; set; }


        public RestSharpClient(string baseUrl,IAuthenticator authenticator = null)
        {
            BaseUrl = baseUrl;
            client = new RestClient(BaseUrl);
            DefaultAuthenticator = authenticator;

            DefaultDateParameterFormat = "yyyy-MM-dd HH:mm:ss";

            if (DefaultAuthenticator!=null)
            {
                client.Authenticator = DefaultAuthenticator;
            }
        }
        
        /// <summary>
        /// 通用执行方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IRestResponse Execute(IRestRequest request)
        {
            request.DateFormat = string.IsNullOrEmpty(request.DateFormat)
                ? DefaultDateParameterFormat
                : request.DateFormat;

            return client.Execute(request);
        }

        /// <summary>
        /// 同步执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public T Execute<T>(IRestRequest request) where T : new()
        {
            request.DateFormat = string.IsNullOrEmpty(request.DateFormat)
                ? DefaultDateParameterFormat
                : request.DateFormat;
            return client.Execute<T>(request).Data;
        }

        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            request.DateFormat = string.IsNullOrEmpty(request.DateFormat)
                ? DefaultDateParameterFormat
                : request.DateFormat;
            return await client.ExecuteAsync(request);
        }
        
        /// <summary>
        /// 异步繁星方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<T> ExecuteAsync<T>(IRestRequest request) where T : new()
        {
            request.DateFormat = string.IsNullOrEmpty(request.DateFormat)
                ? DefaultDateParameterFormat
                : request.DateFormat;
            return (T)await client.ExecuteAsync<T>(request);
        }
    }
}