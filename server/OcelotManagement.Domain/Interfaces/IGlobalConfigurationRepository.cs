using OcelotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Domain.Interfaces
{
    public interface IGlobalConfigurationRepository 
    {
        Task<GlobalConfiguration> GetByIdAsync(string id);
        Task<IEnumerable<GlobalConfiguration>> GetAllAsync();
        Task<IEnumerable<GlobalConfiguration>> GetByProjIdAsync(string projId);
        Task<string> Add(GlobalConfiguration configuration);
        Task Update(GlobalConfiguration configuration);
        Task Remove(GlobalConfiguration configuration);
    }
}
