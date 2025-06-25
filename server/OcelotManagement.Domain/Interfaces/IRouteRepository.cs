using Ocelot.Admin.Api.Core;
using OcelotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Domain.Interfaces
{
    public interface IRouteRepository
    {
        /// <summary>
        /// 获取所有路由信息
        /// </summary>
        /// <param name="proj_id">项目ID</param>
        /// <returns></returns>
        Task<List<Route>> GetAllRoutes(string proj_id);
        Task<List<ListRoute>> GetList(string proj_id);
        Task<Route> GetRouteById(string id);
        Task<string> AddRoute(Route route);
        Task UpdateRoute(Route route);
        Task<bool> DeleteRoute(string id);
        Task UpdateRouteEnabled(string id, bool enabled);
    }
}
