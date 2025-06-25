using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Mappings;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Services
{
    public class GlobalConfigurationService
    {
        private readonly IGlobalConfigurationRepository _repository;

        public GlobalConfigurationService(IGlobalConfigurationRepository repository)
        {
            _repository = repository;
        }

        public async Task<GlobalConfigurationDto> GetByIdAsync(string id)
        {
            var configuration = await _repository.GetByIdAsync(id);
            return MapperConfig.Map<GlobalConfigurationDto>(configuration ?? new GlobalConfiguration());
        }

        public async Task<IEnumerable<GlobalConfigurationDto>> GetAllAsync()
        {
            var configurations = await _repository.GetAllAsync();
            return MapperConfig.Map<IEnumerable<GlobalConfigurationDto>>(configurations);
        }

        public async Task<IEnumerable<GlobalConfigurationDto>> GetByProjIdAsync(string projId)
        {
            var configurations = await _repository.GetByProjIdAsync(projId);
            return MapperConfig.Map<IEnumerable<GlobalConfigurationDto>>(configurations);
        }

        public async Task<string> CreateAsync(string projId, GlobalConfigurationDto configurationDto)
        {
            var configuration = MapperConfig.Map<GlobalConfiguration>(configurationDto);
            configuration.ProjId = projId;
            return await _repository.Add(configuration);
        }

        public async Task UpdateAsync(GlobalConfigurationDto configurationDto)
        {
            var existingConfiguration = MapperConfig.Map<GlobalConfiguration>(configurationDto);
            await _repository.Update(existingConfiguration);
        }

        public async Task DeleteAsync(string id)
        {
            var configuration = await _repository.GetByIdAsync(id);
            if (configuration == null)
            {
                throw new ArgumentException("Configuration not found");
            }

            await _repository.Remove(configuration);

        }
    }
}
