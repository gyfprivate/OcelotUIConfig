using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Mappings;
using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Services
{
    public class ConfigCenterService
    {
        private readonly IConfigCenterRepository _repository;
        private readonly ISnowflake snow;

        public ConfigCenterService(IConfigCenterRepository repository, ISnowflake snow)
        {
            _repository = repository;
            this.snow = snow;
        }

        public async Task<ConfigCenterDto> GetByIdAsync(string id)
        {
            var data = await _repository.GetByIdAsync(id);
            return MapperConfig.Map<ConfigCenterDto>(data);
        }

        public async Task<IEnumerable<ConfigCenterDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            IEnumerable<ConfigCenterDto> result = data.Select(o => MapperConfig.Map<ConfigCenterDto>(o));
            return result;
        }

        public async Task<string> AddAsync(ConfigCenterInputDto configCenter)
        {
            ConfigCenter data = MapperConfig.Map<ConfigCenter>(configCenter);
            return await _repository.AddAsync(data);
        }

        public async Task UpdateAsync(ConfigCenterDto configCenter)
        {
            var data = MapperConfig.Map<ConfigCenter>(configCenter);
            await _repository.UpdateAsync(data);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
