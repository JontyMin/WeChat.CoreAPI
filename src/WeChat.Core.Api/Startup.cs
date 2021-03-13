using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Autofac;
using AutoMapper;
using FluentValidation.AspNetCore;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using WeChat.Core.Api.Authorization;
using WeChat.Core.Api.Filter;
using WeChat.Core.Api.Jobs;
using WeChat.Core.Api.Log;
using WeChat.Core.Api.Middleware;
using WeChat.Core.Api.SetUpService;
using WeChat.Core.Common.Cache;
using WeChat.Core.Common.Helper;
using WeChat.Core.Common.HttpContextUser;
using WeChat.Core.Common.MessageEncryption;
using WeChat.Core.Common.Redis;
using WeChat.Core.Common.WeChat;
using WeChat.Core.Common.WeChat.SendMessageHelper;
using WeChat.Core.IService;
using WeChat.Core.Repository.SqlSugar;
using WeChat.Core.Service;

namespace WeChat.Core.Api
{
    public class Startup
    {
        private readonly string AllowCors = "AllowCors";
        
        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        
        
        /// <summary>
        /// log4net仓储库
        /// </summary>
        public static ILoggerRepository Repository { get; set; }
        public IWebHostEnvironment Env { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
            }).AddFluentValidation().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
            });

            //services.Configure<FormOptions>(option =>
            //{
            //    option.ValueLengthLimit = int.MaxValue;
            //    option.MultipartBodyLengthLimit = int.MaxValue;
            //});

            // 注册AutoMapper服务
            services.AddAutoMapper(typeof(Startup));

            #region Appsettings

            // 注册Appsettings读取类
            services.AddSingleton(new AppSettings(Configuration));

            #endregion

            #region CORS
            services.AddCors(c =>
            {
                //一般采用这种方法
                c.AddPolicy(AllowCors, policy =>
                {
                    policy
                        .WithOrigins("https://wx.lianzhoukaujing.com") //""
                        .AllowAnyOrigin()
                        .AllowAnyHeader()//Ensures that the policy allows any header.
                        .AllowAnyMethod();
                });
            });

            #endregion

            #region Log4Net

            services.AddSingleton<ILoggerHelper, LoggerHelper>();

            Repository = LogManager.CreateRepository("WeChat.Core.Api");
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));
            
            #endregion

            services.AddControllers(option =>
            {
                // 全局异常过滤
                option.Filters.Add(typeof(GlobalExceptionFilter));
            });
            #region Swagger

            services.AddSwaggerGen(t =>
            {
                t.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WeChat.Core.Api",
                    Version = "v1",
                    Description = "Guangzhou lianzhou e-commerce WeChat API",
                    Contact = new OpenApiContact()
                    {
                        Name = "Jonty Wang",
                        Email = "wangjintao@lianzhoukuajing.cn"
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Apache-2.0 License"
                    }
                });

                t.OrderActionsBy(x=>x.RelativePath);
                
                // 为 Swagger JSON and UI设置xml文档注释路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "WeChat.Core.Api.xml");
                t.IncludeXmlComments(xmlPath,true);


                var xmlPathModel = Path.Combine(AppContext.BaseDirectory, "WeChat.Core.Model.xml");
                t.IncludeXmlComments(xmlPathModel,true);

                t.OperationFilter<SecurityRequirementsOperationFilter>();
                t.AddSecurityDefinition("oauth2",new OpenApiSecurityScheme()
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                
            });

            #endregion

            //jwt授权验证
            services.AddAuthorizationJwtSetUp();

            #region Httpcontext

            // HttpContext 注入
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();


            #endregion
            
            // IP请求限制服务
            services.AddIpPolicyRateLimitSetup(Configuration);

            services.AddMemoryCache(options =>
            {
                //缓存满了时候压缩20%的优先级较低的数据
                //options.CompactionPercentage = 0.2;
                //两秒钟查找一次过期项
                //options.ExpirationScanFrequency = TimeSpan.FromSeconds(20);

            });
            
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            //注册Redis
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();

            // Quartz
            services.AddSingleton<QuartzStartup>();
            services.AddTransient<TokenRefreshJob>();
            services.AddTransient<DynamicsDataJob>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。
            services.AddSingleton<QuartzStartup>();
            services.AddSingleton<IJobFactory, IOCJobFactory>();

            // 微信授权注入
            services.AddScoped<IWeChatSDK, WeChatSDK>();
            services.AddScoped<ISendMessage, SendMessage>();

            services.AddSingleton<WXBizMsgCrypt>();
            //BaseDBConfig.ConnectionString = Configuration.GetSection("AppSetting:ConnectionString").Value;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IHostApplicationLifetime applicationLifetime)
        {

            // Ip限流
            app.UseIpRateLimiting();
            
            app.UseMiddleware<IPLimitMiddleware>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Quartz

            var quartz = app.ApplicationServices.GetRequiredService<QuartzStartup>();
            applicationLifetime.ApplicationStarted.Register(() =>
            {
                quartz.Start().Wait(); //网站启动完成执行
            });
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                quartz.Stop();
            });

            #endregion



            //app.UseMvc();

            #region Swagger UI

            app.UseSwagger();
            app.UseSwaggerUI(t =>
            {
                t.SwaggerEndpoint("/swagger/v1/swagger.json", "WeChat.Core.Api v1");
                t.RoutePrefix = string.Empty;
            });
            
            #endregion
            
            app.UseRouting();

            // CORS跨域
            app.UseCors(AllowCors);

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),$"wwwroot/files")),
            //    RequestPath = new PathString("/src")
            //});

            app.UseCustomExceptionMiddleware();

            app.UseAuthentication();
            
            app.UseAuthorization();
            
            app.UseLogReqResponseMiddleware();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// 注册Autofac容器
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }
    }
}
