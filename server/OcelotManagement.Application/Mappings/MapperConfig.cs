using Nelibur.ObjectMapper;
using Ocelot.Admin.Api.Core;
using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Dtos.Ocelot;
using OcelotManagement.Contracts.Common;
using OcelotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Mappings
{

    public static class MapperConfig
    {
        public static void Configure()
        {
            TinyMapper.Bind<ProjInputDto, Project>();
            TinyMapper.Bind<Route, RouterListOutputDto>();
            TinyMapper.Bind<Route, RouterInfoDto>();
            TinyMapper.Bind<RouterInfoDto, Route>();

            TinyMapper.Bind<ApiPagedResult<Project>, ApiPagedResult<ProjDto>>();

            TinyMapper.Bind<Project, ProjDto>();
            TinyMapper.Bind<ProjDto, Project>();

            TinyMapper.Bind<ListRoute, RouterListOutputDto>();
            TinyMapper.Bind<SyncProjInfo, SyncProjInfoDto>(op =>
            {
                op.Bind(o => o.Config, o => o.ConfigCenter);
                op.Bind(o => o.RouteProjs, o => o.RouteProjs);
            });
            TinyMapper.Bind<ConfigCenterInputDto, ConfigCenter>();
            TinyMapper.Bind<ConfigCenter, ConfigCenterDto>();
            TinyMapper.Bind<List<ConfigRouteDto>, List<Route>>();
            TinyMapper.Bind<List<Route>, List<ConfigRouteDto>>();

            TinyMapper.Bind<ConfigCenterInputDto, ConfigCenterDto>();

            //TypeDescriptor.AddAttributes(typeof(ConfigRouteDto), new TypeConverterAttribute(typeof(RouteConverter)));
            TypeDescriptor.AddAttributes(typeof(Route), new TypeConverterAttribute(typeof(RouteConverter)));
            TypeDescriptor.AddAttributes(typeof(GlobalConfiguration), new TypeConverterAttribute(typeof(GlobalConfigurationConverter)));
            TinyMapper.Bind<Route, ConfigRouteDto>();

            TinyMapper.Bind<ConfigCenterDto, ConfigCenter>();

            TinyMapper.Bind<ProjInputDto, ProjDto>();
            TinyMapper.Bind<ProjDto, ProjInputDto>();

            //备份
            TinyMapper.Bind<SettingBackup, SettingBackupDto>();
            TinyMapper.Bind<ConfigCenter, ConsulCenterDto>();
            TinyMapper.Bind<ApiPagedResult<SettingBackup>, ApiPagedResult<SettingBackupDto>>();

            TinyMapper.Bind<GlobalConfiguration, Dtos.GlobalConfigurationDto>();
            TinyMapper.Bind<Dtos.GlobalConfigurationDto, GlobalConfiguration>();

            TinyMapper.Bind<GlobalConfiguration, Dtos.Ocelot.GlobalConfigurationDto>();
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
