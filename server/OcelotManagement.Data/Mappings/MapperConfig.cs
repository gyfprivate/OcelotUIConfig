using Nelibur.ObjectMapper;
using OcelotManagement.Data.Entities;
using OcelotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Data.Mappings
{
    public class MapperConfig
    {
        public static void Configure()
        {
            TinyMapper.Bind<tbUser, User>();
            TinyMapper.Bind<User, tbUser>();
            TinyMapper.Bind<RefreshToken, tbRefreshToken>();
            TinyMapper.Bind<tbRefreshToken, RefreshToken>();
            TinyMapper.Bind<tbConfigCenter, ConfigCenter>();
            TinyMapper.Bind<ConfigCenter, tbConfigCenter>();
            TinyMapper.Bind<Route, tbRoute>();
            TinyMapper.Bind<tbRoute, Route>();
            TinyMapper.Bind<RouteHostPort, tbRouteHostPort>();
            TinyMapper.Bind<Project, tbProject>();
            TinyMapper.Bind<tbProject, Project>();


            TinyMapper.Bind<tbSettingBak, SettingBackup>();
            TinyMapper.Bind<SettingBackup, tbSettingBak>(op =>
            {
                op.Bind(o => o.ProjectId, o => o.ProjId);
                op.Bind(o => o.BackupTime,o=>o.BakTime);
                op.Bind(o => o.BackupContent,o=>o.BakJson);
                op.Bind(o => o.Remark,o=>o.Remark);
            });

            TinyMapper.Bind<tbGlobalConfiguration, GlobalConfiguration>();
            TinyMapper.Bind<GlobalConfiguration, tbGlobalConfiguration>();


        }
        public static T Map<T>(object source)
        {
            return TinyMapper.Map<T>(source);
        }
        public static Tt Map<Ts, Tt>(Ts source, Tt target)
        {
            return TinyMapper.Map<Ts, Tt>(source, target);
        }
    }
}
