using Nelibur.ObjectMapper;
using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Mappings;
using OcelotManagement.Contracts.Common;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<string> AddAsync(ProjInputDto project, int type)
        {
            Project data = new Project();
            MapperConfig.Map(project, data);
            data.Type = type;
            return await _projectRepository.AddAsync(data);
        }

        public async Task DeleteAsync(string id)
        {
            await _projectRepository.DeleteAsync(id);
        }



        public async Task<ApiPagedResult<ProjDto>> GetByTypeAsync(int type, ApiPageArg arg)
        {
            var data = await _projectRepository.GetByTypeAsync(type, arg);
            ApiPagedResult<ProjDto> result = new ApiPagedResult<ProjDto>();
            MapperConfig.Map(data, result);
            return result;
        }
        public async Task UpdateAsync(ProjDto project)
        {
            Project data = new Project();
            TinyMapper.Map(project, data);
            await _projectRepository.UpdateAsync(data);
        }
    }
}
