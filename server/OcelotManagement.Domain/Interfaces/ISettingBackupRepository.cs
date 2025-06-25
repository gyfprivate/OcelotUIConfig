using OcelotManagement.Contracts.Common;
using OcelotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Domain.Interfaces
{
    public class SettingBackupSearchArg : LayuiPageArg
    {
        public string? ConsulId { get; set; }
        public string? ProjId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }

    // 仓储接口 (领域层)
    public interface ISettingBackupRepository
    {
        Task<ApiPagedResult<SettingBackup>> GetAsync(SettingBackupSearchArg arg);
        Task<List<SettingBackup>> GetByProjectIdAsync(string projectId);
        Task<string> Add(SettingBackup backup);
        void Update(SettingBackup backup);
        void Delete(SettingBackup backup);
    }
}
