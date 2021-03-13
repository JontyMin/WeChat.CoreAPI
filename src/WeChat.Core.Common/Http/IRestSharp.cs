using System;
using System.Threading.Tasks;
using RestSharp;

namespace WeChat.Core.Common.Http
{
    public interface IRestSharp
    {
        /// <summary>
        /// 同步执行方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IRestResponse Execute(IRestRequest request);

        /// <summary>
        /// 同步执行方法
        /// </summary>
        /// <typeparam name="T">返回值</typeparam>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        T Execute<T>(IRestRequest request) where T : new();

        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        Task<IRestResponse> ExecuteAsync(IRestRequest request);

        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<T> ExecuteAsync<T>(IRestRequest request) where T : new();
    }
}