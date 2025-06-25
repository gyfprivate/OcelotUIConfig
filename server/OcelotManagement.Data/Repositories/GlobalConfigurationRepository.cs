using Microsoft.Extensions.Configuration;
using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Data.Entities;
using OcelotManagement.Data.Mappings;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Data.Repositories
{
    internal class GlobalConfigurationRepository : IGlobalConfigurationRepository
    {
        private readonly Repository<tbGlobalConfiguration> rep;
        private readonly ISnowflake snow;

        public GlobalConfigurationRepository(Repository<tbGlobalConfiguration> rep, ISnowflake snow)
        {
            this.rep = rep;
            this.snow = snow;
        }
        public async Task<string> Add(GlobalConfiguration configuration)
        {
            var data = MapperConfig.Map<tbGlobalConfiguration>(configuration);
            if (string.IsNullOrEmpty(data.ProjId) || data.ProjId == "0")
            {
                data.Id = "0";
                data.ProjId = "0";
                await rep.InsertOrUpdateAsync(data);
                return data.Id;
            }
            else
            {
                data.Id = snow.GetSnowID();
                await rep.InsertAsync(data);
                return data.Id;
            }
        }

        public Task<IEnumerable<GlobalConfiguration>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GlobalConfiguration> GetByIdAsync(string id)
        {
            var data = await rep.GetFirstAsync(o => o.Id == id);
            return MapperConfig.Map<GlobalConfiguration>(data ?? new tbGlobalConfiguration() { ProjId = "0" });
        }

        public Task<IEnumerable<GlobalConfiguration>> GetByProjIdAsync(string projId)
        {
            throw new NotImplementedException();
        }

        public Task Remove(GlobalConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public async Task Update(GlobalConfiguration configuration)
        {
            tbGlobalConfiguration data = new();
            MapperConfig.Map(configuration, data);
            await rep.AsUpdateable(data)
                .IgnoreColumns(o => new { o.ProjId })
                .WhereColumns(o => new { o.Id })
                .ExecuteCommandAsync();
        }
    }
}
