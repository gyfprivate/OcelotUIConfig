using OcelotManagement.Data.Entities;
using OcelotManagement.Data.Mappings;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using OracleInternal.Secure.Network;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Data.Repositories
{
    internal class ConfigurationRepository : IConfigurationRepository
    {
        private readonly IRouteRepository repRoute;
        private readonly Repository<tbConfigCenter> rep;
        private readonly Repository<tbGlobalConfiguration> repGlobal;
        private readonly Repository<tbSettingBak> repBak;

        public ConfigurationRepository(IRouteRepository repRoute, Repository<tbConfigCenter> rep, Repository<tbGlobalConfiguration> repGlobal, Repository<tbSettingBak> repBak)
        {
            this.repRoute = repRoute;
            this.rep = rep;
            this.repGlobal = repGlobal;
            this.repBak = repBak;
        }
        /// <summary>
        /// 获取项目所有配置信息
        /// </summary>
        /// <param name="projId"></param>
        /// <returns></returns>
        public async Task<ConfigurationToSave> GetAllConfigurationsAsync(string projId)
        {
            var routes = await repRoute.GetAllRoutes(projId);

            var q = rep.AsQueryable()
                .InnerJoin<tbProject>((a, b) => a.Id == b.ConfigId)
                .Where((a, b) => b.Id == projId)
                .Select((a, b) => new ConfigCenter() { Host = a.Host, Port = a.Port, Key = a.Key, Path = a.Path });
            var configCenter = await q.FirstAsync();


            ConfigurationToSave result = new ConfigurationToSave()
            {
                Routes = routes,
                ConfigCenter = configCenter
            };
            var globals = await repGlobal.AsQueryable().Where(o => o.ProjId == "0" || o.ProjId == projId).ToListAsync();
            if (globals != null)
            {
                //只有一个全局配置
                if (globals.Count == 1)
                {
                    GlobalConfiguration global = MapperConfig.Map<GlobalConfiguration>(globals.First());
                    result.Global = global;
                }
                else
                {
                    //有多个全局配置
                    GlobalConfiguration global = MapperConfig.Map<GlobalConfiguration>(globals.First(o => o.ProjId == projId));
                    result.Global = global;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">回滚备份ID</param>
        /// <returns></returns>
        public async Task<ConfigurationToRollBack> GetRollBackConfigurationsAsync(string id)
        {
            var q = repBak.AsQueryable()
                .Where(o => o.Id == id)
                .Select(o => new
                {
                    o.BakJson,
                    ConfigCenter = SqlFunc.Subqueryable<tbConfigCenter>()
                    .Where(s1 => SqlFunc.Subqueryable<tbProject>().Where(s2 => s2.Id == o.ProjId && s2.ConfigId == s1.Id).Any())
                    .First()
                    //.First(s1 => new ConfigCenter()
                    //{
                    //    Host = SqlFunc.IsNull(s1.Host, ""),
                    //    Port = SqlFunc.IsNull(s1.Port, 0),
                    //    Key = SqlFunc.IsNull(s1.Key, ""),
                    //    Path = SqlFunc.IsNull(s1.Path, "")
                    //})
                })
                .MergeTable();
            var data = await q.FirstAsync();
            ConfigurationToRollBack result = new();
            result.Json = data.BakJson;
            result.ConfigCenter = new ConfigCenter()
            {
                Host = data.ConfigCenter?.Host ?? "",
                Port = data.ConfigCenter?.Port ?? 0,
                Path = data.ConfigCenter?.Path ?? "",
                Key = data.ConfigCenter?.Key ?? "",
            };
            return result;
        }
    }
}
