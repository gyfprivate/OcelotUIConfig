using Nelibur.ObjectMapper;
using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Data.Entities;
using OcelotManagement.Data.Mappings;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Data.Repositories
{
    class ConfigCenterRepository : IConfigCenterRepository
    {
        private readonly Repository<tbConfigCenter> rep;
        private readonly ISnowflake snow;

        public ConfigCenterRepository(Repository<tbConfigCenter> rep, ISnowflake snow)
        {
            this.rep = rep;
            this.snow = snow;
        }
        public async Task<string> AddAsync(ConfigCenter configCenter)
        {
            configCenter.Id = snow.GetSnowID();
            var data = MapperConfig.Map<tbConfigCenter>(configCenter);
            await rep.AsInsertable(data).ExecuteCommandAsync();
            return configCenter.Id;
        }

        public async Task DeleteAsync(string id)
        {
            await rep.AsDeleteable().Where(o => o.Id == id).ExecuteCommandAsync();
        }

        public async Task<IEnumerable<ConfigCenter>> GetAllAsync()
        {
            var data = await rep.AsQueryable().ToListAsync();

            return data.ConvertAll(o => TinyMapper.Map<ConfigCenter>(o));
        }

        public async Task<ConfigCenter> GetByIdAsync(string id)
        {
            var data = await rep.AsQueryable()
                   .FirstAsync(o => o.Id == id);
            return TinyMapper.Map<ConfigCenter>(data);
        }

        public async Task UpdateAsync(ConfigCenter configCenter)
        {
            var data = MapperConfig.Map<tbConfigCenter>(configCenter);

            await rep.AsUpdateable(data)
                .Where(o => o.Id == configCenter.Id)
                .IgnoreColumns(o => new { o.Id }) // 忽略主键
                .ExecuteCommandAsync();
        }
    }
}
