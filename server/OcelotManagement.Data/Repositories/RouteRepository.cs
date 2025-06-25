using Microsoft.AspNetCore.Server.Kestrel.Core.Internal;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Admin.Api.Core;
using OcelotManagement.Contracts.Common;
using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Data.Entities;
using OcelotManagement.Data.Mappings;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Data.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        // 这里假设我们使用 Entity Framework Core 进行数据访问
        private readonly Repository<tbRoute> rep;
        private readonly Repository<tbRouteHostPort> repHosts;
        private readonly Repository<tbSyncProjRouteMap> reMap;
        private readonly ISnowflake snow;

        public RouteRepository(Repository<tbRoute> rep, Repository<tbRouteHostPort> repHosts, Repository<tbSyncProjRouteMap> reMap, ISnowflake snow)
        {
            this.rep = rep;
            this.repHosts = repHosts;
            this.reMap = reMap;
            this.snow = snow;
        }

        public async Task<string> AddRoute(Route route)
        {
            route.Id = snow.GetSnowID();
            tbRoute data = new();
            MapperConfig.Map(route, data);

            //准备host列表同步插入
            var hosts = new List<tbRouteHostPort>();
            if (hosts != null && hosts.Count > 0)
            {
                hosts = route.Hosts.ConvertAll(o => MapperConfig.Map<tbRouteHostPort>(o));
                hosts.ForEach(o => o.Id = snow.GetSnowID());
                hosts.ForEach(o => o.RouteId = data.Id);
            }
            data.Enabled = true;
            await rep.TranAsync(async () =>
            {
                if (hosts != null && hosts.Count > 0)
                    await repHosts.InsertRangeAsync(hosts);
                await rep.InsertAsync(data);
            }, e =>
            {
                Console.WriteLine($"增加路由失败 {e.Message}");
            });
            return route.Id;
        }

        public async Task<bool> DeleteRoute(string id)
        {
            return await rep.DeleteAsync(o => o.Id == id);
        }

        public async Task<List<Route>> GetAllRoutes(string proj_id)
        {
            var q = rep.AsQueryable()
                .Where(o => SqlFunc.Subqueryable<tbSyncProjRouteMap>().Where(s1 => s1.ProjId == proj_id && s1.RouteId == o.ProjectId).Any())
                .OrderBy(o => o.Sort ?? 0)
                .Select(o => new Route() { Id = o.Id.SelectAll(), Hosts = SqlFunc.Subqueryable<tbRouteHostPort>().Where(s2 => s2.RouteId == o.Id).ToList(s2 => new RouteHostPort() { Host = s2.Host, Id = s2.Id, Port = s2.Port }) })
                .MergeTable();
            return await q.ToListAsync();
        }

        public async Task<List<ListRoute>> GetList(string proj_id)
        {
            var q = rep.AsQueryable()
                .WhereIF(!string.IsNullOrEmpty(proj_id), o => o.ProjectId == proj_id)
                .OrderBy(o => o.Sort ?? 0)
                .Select(o => new ListRoute() { Id = o.Id, DownstreamPathTemplate = o.DownstreamPathTemplate, UpstreamPathTemplate = o.UpstreamPathTemplate, Enabled = o.Enabled });
            return await q.ToListAsync();
        }

        public async Task<Route> GetRouteById(string id)
        {
            var data = await rep.AsQueryable()
                .Where(o => o.Id == id)
                .Select(o =>
            new Route()
            {
                Id = o.Id.SelectAll(),
                Hosts = SqlFunc.Subqueryable<tbRouteHostPort>().Where(s1 => s1.RouteId == o.Id).ToList(s1 => new RouteHostPort() { Id = s1.Id.SelectAll() })
            })
                .FirstAsync(o => o.Id == id);
            return data;
        }
        public async Task UpdateRoute(Route route)
        {
            tbRoute data = new();
            MapperConfig.Map(route, data);

            var hosts = new List<tbRouteHostPort>();
            if (route.Hosts != null && route.Hosts.Count > 0)
            {
                hosts = route.Hosts!.ConvertAll(o => MapperConfig.Map<tbRouteHostPort>(o));
                hosts.ForEach(o =>
                {
                    if (string.IsNullOrEmpty(o.Id) || o.Id == "0")
                        o.Id = snow.GetSnowID();
                    o.RouteId = data.Id;
                });
            }

            await rep.TranAsync(async () =>
            {
                //await rep.Context.Deleteable<tbRouteHostPort>().Where(o => o.RouteId == route.Id).ExecuteCommandAsync();
                //await rep.Context.Insertable(hosts).ExecuteCommandAsync();
                await repHosts.DeleteAsync(o => o.RouteId == route.Id);
                await repHosts.InsertRangeAsync(hosts);
                await rep.AsUpdateable(data).IgnoreColumns(o => new { o.Enabled }).ExecuteCommandAsync();
            }, e =>
            {
                Console.WriteLine("更新路由信息出错：" + e.Message);
            });
        }

        public async Task UpdateRouteEnabled(string id, bool enabled)
        {
            await rep.UpdateAsync(o => new tbRoute() { Enabled = enabled }, o => o.Id == id);
        }
    }
}
