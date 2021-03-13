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
        /// log4net�ִ���
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

            // ע��AutoMapper����
            services.AddAutoMapper(typeof(Startup));

            #region Appsettings

            // ע��Appsettings��ȡ��
            services.AddSingleton(new AppSettings(Configuration));

            #endregion

            #region CORS
            services.AddCors(c =>
            {
                //һ��������ַ���
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
                // ȫ���쳣����
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
                
                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "WeChat.Core.Api.xml");
                t.IncludeXmlComments(xmlPath,true);


                var xmlPathModel = Path.Combine(AppContext.BaseDirectory, "WeChat.Core.Model.xml");
                t.IncludeXmlComments(xmlPathModel,true);

                t.OperationFilter<SecurityRequirementsOperationFilter>();
                t.AddSecurityDefinition("oauth2",new OpenApiSecurityScheme()
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
                
            });

            #endregion

            //jwt��Ȩ��֤
            services.AddAuthorizationJwtSetUp();

            #region Httpcontext

            // HttpContext ע��
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();


            #endregion
            
            // IP�������Ʒ���
            services.AddIpPolicyRateLimitSetup(Configuration);

            services.AddMemoryCache(options =>
            {
                //��������ʱ��ѹ��20%�����ȼ��ϵ͵�����
                //options.CompactionPercentage = 0.2;
                //�����Ӳ���һ�ι�����
                //options.ExpirationScanFrequency = TimeSpan.FromSeconds(20);

            });
            
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            //ע��Redis
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();

            // Quartz
            services.AddSingleton<QuartzStartup>();
            services.AddTransient<TokenRefreshJob>();
            services.AddTransient<DynamicsDataJob>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//ע��ISchedulerFactory��ʵ����
            services.AddSingleton<QuartzStartup>();
            services.AddSingleton<IJobFactory, IOCJobFactory>();

            // ΢����Ȩע��
            services.AddScoped<IWeChatSDK, WeChatSDK>();
            services.AddScoped<ISendMessage, SendMessage>();

            services.AddSingleton<WXBizMsgCrypt>();
            //BaseDBConfig.ConnectionString = Configuration.GetSection("AppSetting:ConnectionString").Value;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IHostApplicationLifetime applicationLifetime)
        {

            // Ip����
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
                quartz.Start().Wait(); //��վ�������ִ��
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

            // CORS����
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
        /// ע��Autofac����
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }
    }
}
