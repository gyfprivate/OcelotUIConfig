using OcelotManagement.Contracts.Common;
using OcelotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OcelotManagement.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<SyncProjInfo> GetInfoByIdAsync(string id);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<ApiPagedResult<Project>> GetByTypeAsync(int type, ApiPageArg arg);
        Task<string> AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(string id);
        Task SetProjRoutes(string projId, List<string> routeIds);
        Task SetProjConfig(string projId, string configId);
    }
}
