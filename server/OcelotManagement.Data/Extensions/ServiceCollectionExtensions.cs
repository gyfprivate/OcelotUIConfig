using OcelotManagement.Data.Models;
using OcelotManagement.Data.Services;
using OcelotManagement.Infrastructure.Services;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using OcelotManagement.Domain.Interfaces;
using OcelotManagement.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using OcelotManagement.Domain.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using OcelotManagement.Contracts.Common;
using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Data.Entities;
using OcelotManagement.Data.Services.Database;

namespace OcelotManagement.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 数据库 仓储 Repository
        /// 雪花id snow_flake
        /// 连接字符串 ConnectionString
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSQLRepository(this IServiceCollection services, IConfiguration configuration, string connStr = "ConnectionString")
        {
            services.AddScoped(typeof(Repository<>)); //仓储
            services.AddScoped<DBFactory>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IConfigCenterRepository, ConfigCenterRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IRouteRepository, RouteRepository>();
            services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
            services.AddTransient<ISettingBackupRepository, SettingBackupRepository>();
            services.AddTransient<IGlobalConfigurationRepository, GlobalConfigurationRepository>();


            services.AddTransient<ISnowflake, MySnowflake>();
            //services.AddScoped<DBFactory>(); //数据库
            //services.AddScoped<DBFactoryBase, DBFactory>(); //数据库
            services.Configure<ConnectionConfig>(configuration.GetSection("ConnectionString")); //数据库连接字符串

            var op = new OptionsSnowFlake();
            configuration.Bind("snow_flake", op);
            MySnowflake.AddSnowflakeOption(op); //雪花id初始化
            services.Configure<ConnectionConfig>(configuration.GetSection(connStr)); //数据库连接字符串
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        public static async void InitSql(this IServiceProvider services)
        {
            using (var sc = services.CreateScope())
            {
                var rep = sc.ServiceProvider.GetService<Repository<tbUser>>();
                var entityTypes = Assembly.GetAssembly(typeof(tbUser)).GetTypes().Where(o => !string.IsNullOrEmpty(o.Namespace) && o.Namespace.Equals(typeof(tbUser).Namespace) && o.IsClass && !o.IsEnum && !o.IsInterface)?.ToArray();
                rep.Context.CodeFirst.InitTables(entityTypes);

                //seeds 
                var _userRepository = sc.ServiceProvider.GetService<IUserRepository>();
                var u = await _userRepository.GetByUsernameAsync("admin");
                var snow = sc.ServiceProvider.GetService<ISnowflake>();
                if (u == null)
                {
                    // 创建用户实体
                    var user = new User
                    {
                        Id = snow.GetSnowID(),
                        Username = "admin",
                        PassWord = "123456",
                        //Email = dto.Email,
                        //Role = dto.Role,
                        CreatedAt = DateTime.UtcNow
                    };



                    // 保存用户到数据库
                    await _userRepository.AddAsync(user);

                }
            }
        }
    }
}