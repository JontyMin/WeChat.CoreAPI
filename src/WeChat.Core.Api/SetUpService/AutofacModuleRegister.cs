using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;

namespace WeChat.Core.Api.SetUpService
{
    public class AutofacModuleRegister:Autofac.Module
    {
        /// <summary>
        /// 重写Load函数
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            // 注册服务
            var assemblysServices = Assembly.Load("WeChat.Core.Service");
            builder.RegisterAssemblyTypes(assemblysServices)
                .InstancePerDependency() // 默认模式，每次调用都会重新实例化对象；每次请求都创建一个新对象
                .AsImplementedInterfaces() // 以接口方式注入，注入的类所有的公共接口作为服务(除了释放资源)
                .EnableInterfaceInterceptors();// 引用Autofac.Extras.DynamicProxy;应用拦截器
            
            // 注册Repository
            var assemblysRepository = Assembly.Load("WeChat.Core.Repository");
            builder.RegisterAssemblyTypes(assemblysRepository)
                .InstancePerDependency()
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors();
        }
    }
}