using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Mappings;
using OcelotManagement.Contracts.Common;
using OcelotManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Services
{
    public class SettingBackupService
    {
        private readonly ISettingBackupRepository repSettingBackup;

        public SettingBackupService(ISettingBackupRepository rep)
        {
            this.repSettingBackup = rep;
        }
        public async Task CreateAsync(SettingBackupDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiPagedResult<SettingBackupDto>> GetAsync(SettingBackupSearchArg arg)
        {
            var data = await repSettingBackup.GetAsync(arg);
            ApiPagedResult<SettingBackupDto> dto = new();
            MapperConfig.Map(data, dto);
            return dto;
        }

        public async Task GetByProjectIdAsync(string projectId)
        {
            throw new NotImplementedException();
        }
    }
}
