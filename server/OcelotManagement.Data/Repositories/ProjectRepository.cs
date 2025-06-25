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
    internal class ProjectRepository : IProjectRepository
    {
        private readonly Repository<tbProject> rep;
        private readonly Repository<tbSyncProjRouteMap> repRouteMap;
        private readonly ISnowflake snow;

        public ProjectRepository(Repository<tbProject> rep, Repository<tbSyncProjRouteMap> repRouteMap, ISnowflake snow)
        {
            this.rep = rep;
            this.repRouteMap = repRouteMap;
            this.snow = snow;
        }

        public async Task<string> AddAsync(Project project)
        {
            project.Id = snow.GetSnowID();
            tbProject data = new tbProject();
            MapperConfig.Map(project, data);
            var now = DateTime.Now;
            data.CreateTime = now;
            data.UpdateTime = now;
            await rep.AsInsertable(data).ExecuteCommandAsync();
            return project.Id;
        }

        public async Task DeleteAsync(string id)
        {
            await rep.AsDeleteable().Where(o => o.Id == id).ExecuteCommandAsync();
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            var data = await rep.AsQueryable().ToListAsync();
            IEnumerable<Project> result = data.Select(o => MapperConfig.Map<Project>(o));
            return result;
        }

        public async Task<SyncProjInfo> GetInfoByIdAsync(string id)
        {
            var q = rep.AsQueryable()
                .Where(o => o.Id == id)
                .Select(o => new SyncProjInfo()
                {

                    Id = o.Id.SelectAll(),
                    Config = SqlFunc.Subqueryable<tbConfigCenter>().Where(s1 => s1.Id == o.ConfigId).First<ConfigCenter>(s1 => new ConfigCenter() { Id = s1.Id.SelectAll() }),
                    RouteProjs = SqlFunc.Subqueryable<tbProject>().Where(s2 =>
                    SqlFunc.Subqueryable<tbSyncProjRouteMap>().Where(s3 => s3.ProjId == o.Id && s3.RouteId == s2.Id).Any()
                    ).ToList<Project>(s2 => new Project() { Id = s2.Id.SelectAll() })
                })
                .MergeTable();

            var data = await q.FirstAsync();
            return data;
        }

        public async Task<ApiPagedResult<Project>> GetByTypeAsync(int type, ApiPageArg arg)
        {
            RefAsync<int> total = 0;
            var data = await rep.AsQueryable()
                .Where(o => o.Type == type)
                .OrderBy(o => o.OrderIndex)
                .Select(o => new Project() { Id = o.Id.SelectAll() })
                .ToPageListAsync(arg.CurrentPage, arg.PageSize, total);
            var result = new ApiPagedResult<Project>() { CurrentPage = arg.PageSize, PageSize = arg.PageSize, TotalCount = total, Items = data };
            return result;
        }

        public async Task SetProjConfig(string projId, string configId)
        {
            var r = await rep.UpdateAsync(o => new tbProject() { ConfigId = configId }, o => o.Id == projId);
        }

        public async Task SetProjRoutes(string projId, List<string> routeIds)
        {
            var maps = routeIds?.ConvertAll(o => new tbSyncProjRouteMap() { ProjId = projId, RouteId = o }) ?? new List<tbSyncProjRouteMap>();
            await rep.TranAsync(async () =>
            {
                await repRouteMap.DeleteAsync(o => o.ProjId == projId);
                await repRouteMap.InsertRangeAsync(maps);
            }, e => { });
        }

        public async Task UpdateAsync(Project project)
        {
            tbProject data = new();
            MapperConfig.Map(project, data);
            data.UpdateTime = DateTime.Now;
            await rep.AsUpdateable(data)
                .Where(o => o.Id == project.Id)
                .IgnoreColumns(o => new { o.Id, o.CreateTime, o.Type }) // 忽略主键
                .ExecuteCommandAsync();
        }
    }
}
