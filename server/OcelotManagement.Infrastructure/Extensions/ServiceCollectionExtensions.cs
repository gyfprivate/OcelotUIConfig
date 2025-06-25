using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Infrastructure.Middleware;
using OcelotManagement.Infrastructure.Models;
using OcelotManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 使用跨域
        /// </summary>
        /// <param name="app"></param>
        public static void UseCors(this WebApplication app)
        {
            app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(_ => true)); //跨域
            app.UseCors("AnyOrigin");
            app.UseMiddleware<PreOptionRequestMiddleware>(); //天坑 options 请求 prefilght
        }
      
        /// <summary>
        /// 使用html
        /// </summary>
        /// <param name="app"></param>
        public static void UseHTML(this WebApplication app)
        {
            //主页是html
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            //设置首页，我希望用户打开`localhost`访问到的是`wwwroot`下的Index.html文件
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".less"] = "text/css";
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = o =>
                {
                    if (Path.GetExtension(o.File.Name).ToLower() == ".html")
                        o.Context.Response.Headers.Add("cache-control", "no-cache");
                },
                ContentTypeProvider = provider
            });
            try
            {

                //图片文件路径浏览器
                app.UseDirectoryBrowser(new DirectoryBrowserOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                     System.IO.Path.Combine(Environment.CurrentDirectory, "wwwroot")
                        ),
                    RequestPath = "/browser"
                });
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
        /// <summary>
        /// 添加跨域
        /// </summary>
        /// <param name="builder"></param>
        public static void AddOcelotCors(this IServiceCollection services)
        {
            //跨域
            services.AddCors(options => options.AddPolicy("AnyOrigin", o => o.AllowAnyOrigin()));
        }
        /// <summary>
        /// 增加swagger
        /// Authorization:bearer 认证
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },        new string[] { }
                    }
                });
                //xml文档的绝对路径
                var files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                //true显示 控制器注释
                foreach (var file in files)
                    option.IncludeXmlComments(file, true);
                //排序
                option.OrderActionsBy(o => o.RelativePath);
                //.net Core Swagger报错 the same schemaid is already used for type swagger
                option.CustomSchemaIds(CustomSchemaIdSelector);
            }
            );
        }
        /// <summary>
        /// https://www.cnblogs.com/shanfeng1000/p/13476831.html
        /// CustomSchemaIds
        /// CustomSchemaIds方法用于自定义SchemaId，Swashbuckle中的每个Schema都有唯一的Id，框架会使用这个Id匹配引用类型，因此这个Id不能重复。
        /// 默认情况下，这个Id是根据类名得到的（不包含命名空间），因此，当我们有两个相同名称的类时，Swashbuckle就会报错：　　
        /// System.InvalidOperationException: Can't use schemaId "$XXXXX" for type "$XXXX.XXXX". The same schemaId is already used for type "$XXXX.XXXX.XXXX"
        /// 就是类似上面的异常，一般时候我们都得去改类名，有点不爽，这时就可以使用这个方法自己自定义实现SchemaId的获取，比如，我们自定义实现使用类名的全限定名（包含命名空间）来生//成/SchemaId，上面的异常就没有了：　　　
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        private static string CustomSchemaIdSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType) return modelType.FullName.Replace("[]", "Array");

            var prefix = modelType.GetGenericArguments()
                .Select(genericArg => CustomSchemaIdSelector(genericArg))
                .Aggregate((previous, current) => previous + current);

            var result = prefix + modelType.FullName.Split('`').First();

            result = result.Replace("OrgOpenApiToolsModel", ""); //自己互怼

            return result;
        }
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = o =>
                        {
                            var key = o.SecurityToken.SecurityKey;
                            //o.HttpContext.Items[KID_NAME] = o.SecurityToken.SigningKey.KeyId;
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = o =>
                        {
                            Console.WriteLine("OnAuthenticationFailed");
                            Console.WriteLine(o.Exception?.ToString());
                            return Task.CompletedTask;
                        },
                        OnChallenge = o =>
                        {
                            Console.WriteLine("OnChallenge");
                            Console.WriteLine(o.AuthenticateFailure?.ToString());
                            return Task.CompletedTask;
                        },
                        OnForbidden = o =>
                        {
                            Console.WriteLine("OnForbidden");
                            //Console.WriteLine(JsonConvert.SerializeObject(o.Result));
                            return Task.CompletedTask;
                        },
                        //OnMessageReceived = o =>
                        //{
                        //    Console.WriteLine("OnMessageReceived");
                        //    Console.WriteLine(JsonConvert.SerializeObject(o.HttpContext));
                        //    return Task.CompletedTask;
                        //}
                    };
                });

            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
