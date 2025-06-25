using Newtonsoft.Json;
using OcelotManagement.Application.Dtos.Ocelot;
using OcelotManagement.Application.Mappings;
using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OcelotManagement.Application.Services
{
    // 配置同步服务
    public class ConfigurationSyncService
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ISettingBackupRepository settingBackupRepository;
        private readonly IConsulService _consulService;

        //private readonly ILogger<ConfigurationSyncService> _logger;

        public ConfigurationSyncService(
            IConfigurationRepository configurationRepository,
            ISettingBackupRepository settingBackupRepository,
            IConsulService consulService
            //,ilogger<ConfigurationSyncService> logger
            )
        {
            _configurationRepository = configurationRepository;
            this.settingBackupRepository = settingBackupRepository;
            _consulService = consulService;
            //_logger = logger;
        }

        public async Task<string> GetConfigurationByProjIdAsync(string projId)
        {
            // 获取所有配置
            var configuration = await _configurationRepository.GetAllConfigurationsAsync(projId);
            return ConvertConfigurationToJson(configuration);
        }

        /// <summary>
        /// 将配置转换为json字符串,并保存备份记录,同时将配置写入consul中
        /// </summary>
        /// <param name="projId"></param>
        /// <returns></returns>
        public async Task SyncConfigurationsToConsulAsync(string projId)
        {
            try
            {
                // 获取所有配置
                var configuration = await _configurationRepository.GetAllConfigurationsAsync(projId);
                string json = ConvertConfigurationToJson(configuration);

                await _consulService.WriteKvAsync(configuration.ConfigCenter.Host, configuration.ConfigCenter.Port, configuration.ConfigCenter.Key, configuration.ConfigCenter.Path, json);

                //同步日志
                string id = await settingBackupRepository.Add(new Domain.Entities.SettingBackup() { ProjectId = projId, BackupTime = DateTime.Now, BackupContent = json, Remark = "同步备份" });

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Failed to sync configurations to Consul");
                throw;
            }
        }

        /// <summary>
        /// 回滚配置到之前的版本
        /// </summary>
        /// <param name="bakId"></param>
        /// <returns></returns>
        public async Task RollBackConfigurationsAsync(string bakId)
        {
            try
            {
                // 获取回滚配置
                var configuration = await _configurationRepository.GetRollBackConfigurationsAsync(bakId);
                string json = configuration.Json;

                await _consulService.WriteKvAsync(configuration.ConfigCenter.Host, configuration.ConfigCenter.Port, configuration.ConfigCenter.Key, configuration.ConfigCenter.Path, json);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Failed to sync configurations to Consul");
                throw;
            }
        }

        public static string ConvertConfigurationToJson(ConfigurationToSave configuration)
        {
            List<ConfigRouteDto> routes = new List<ConfigRouteDto>();
            {
                routes.AddRange(configuration.Routes.Select(o =>
                {
                    ConfigRouteDto r = null;
                    r = MapperConfig.Map<ConfigRouteDto>(o);
                    return r;
                }));
            }
            var global = MapperConfig.Map<GlobalConfigurationDto>(configuration.Global);
            return ConvertRouteToJson(routes, global);
        }

        public static string ConvertRouteToJson(List<ConfigRouteDto> routes, GlobalConfigurationDto global)
        {
            var ocelotConfig = new OcelotConfigurationDto();
            ocelotConfig.Routes = routes;
            var jSetting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                // 确保没有启用TypeNameHandling
                TypeNameHandling = TypeNameHandling.None,
                // 可以添加其他必要设置
                Formatting = Newtonsoft.Json.Formatting.Indented,
            };
            ocelotConfig.GlobalConfiguration = global;
            var json = JsonConvert.SerializeObject(ocelotConfig, jSetting);
            return json;
        }
    }
}
