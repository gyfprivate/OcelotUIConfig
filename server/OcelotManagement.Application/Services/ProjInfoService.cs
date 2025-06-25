using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Mappings;
using OcelotManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Services
{
    public class ProjInfoService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjInfoService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<SyncProjInfoDto> GetByIdAsync(string id)
        {
            var data = await _projectRepository.GetInfoByIdAsync(id);
            SyncProjInfoDto result = new SyncProjInfoDto();
            MapperConfig.Map(data, result);
            return result;
        }

        public async Task SaveConfig(ProjConfigSaveDto arg)
        {
            await _projectRepository.SetProjConfig(arg.ProjId, arg.ConfigId);
        }

        public async Task SaveRoutes(ProjRouteSaveDto arg)
        {
            await _projectRepository.SetProjRoutes(arg.ProjId, arg.RouteIds);
        }
    }
}
