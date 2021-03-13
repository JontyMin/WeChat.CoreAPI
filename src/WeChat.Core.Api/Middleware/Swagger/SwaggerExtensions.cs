using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WeChat.Core.Common.Helper;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using WeChat.Core.Api.Middleware.Swagger;

namespace WeChat.Core.Common.Swagger
{
    public static class SwaggerExtensions
    {
        
        private static readonly string Version = AppSettings.app(new string[] { "AppSettings", "ApiVersion"});

        /// <summary>
        /// Swagger描述信息
        /// </summary>
        private static readonly string description = @"Guangzhou lianzhou e-commerce Core API";

        /// <summary>
        /// Swagger分组信息，遍历使用
        /// </summary>
        private static readonly List<SwaggerApiInfo> ApiInfos = new List<SwaggerApiInfo>()
        {
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupNameV1,
                Name = "供应商接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = Version,
                    Title = "供应商接口",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupNameV2,
                Name = "公众号接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = Version,
                    Title = "公众号接口",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupNameV3,
                Name = "uni-app接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = Version,
                    Title = "uni-app接口",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupNameV4,
                Name = "授权接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = Version,
                    Title = "授权接口",
                    Description = description
                }
            }
        };
        /// <summary>
        /// AddSwagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                //options.SwaggerDoc("v1",new OpenApiInfo()
                //{
                //    Version = "1.0.0",
                //    Title = "JontyBlog API",
                //    Description = "this is a api doc"
                //});

                // 遍历并应用Swagger分组信息
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerDoc(x.UrlPrefix, x.OpenApiInfo);
                    
                });

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Jonty.Blog.HttpApi.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Jonty.Blog.Domain.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Jonty.Blog.Application.Contracts.xml"));

                #region 小绿锁，JWT身份认证

                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT模式授权，请输入Bearer { Token } 进行身份认证",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };

                options.AddSecurityDefinition("oauth2", security);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, new List<string>() } });
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                #endregion
                //应用Controller的API文档描述信息
                //options.DocumentFilter<SwaggerDocumentFilter>();
            });
        }
        /// <summary>
        /// UseSwaggerUI
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            //SwaggerDoc SwaggerEndpoint 对应api版本号相同 v1-v1

            //app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                //遍历分组信息,生成json
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerEndpoint($"/swagger/{x.UrlPrefix}/swagger.json", x.Name);
                });
                
                // 模型的默认扩展深度，设置为-1完全隐藏模型
                options.DefaultModelExpandDepth(-1);
                //API文档仅展开标记
                options.DocExpansion(DocExpansion.List);
                // API前缀设置为空
                options.RoutePrefix = string.Empty;
                // API页面Title
                options.DocumentTitle = "LZ.CoreAPI";

            });
        }
    }

    internal class SwaggerApiInfo
    {
        /// <summary>
        /// Url前缀
        /// </summary>
        public string UrlPrefix { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// <see cref="Microsoft.OpenApi.Models.OpenApiInfo"/>
        /// </summary>
        public OpenApiInfo OpenApiInfo { get; set; }

      
    }
}

