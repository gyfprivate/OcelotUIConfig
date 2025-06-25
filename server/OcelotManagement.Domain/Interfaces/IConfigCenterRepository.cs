using OcelotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Domain.Interfaces
{
    public interface IConfigCenterRepository
    {
        Task<ConfigCenter> GetByIdAsync(string id);
        Task<IEnumerable<ConfigCenter>> GetAllAsync();
        Task<string> AddAsync(ConfigCenter configCenter);
        Task UpdateAsync(ConfigCenter configCenter);
        Task DeleteAsync(string id);
    }
}
