using OcelotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Domain.Interfaces
{
    // 配置仓储接口
    public interface IConfigurationRepository
    {
        /// <summary>
        ///  获取指定项目的所有配置。
        /// </summary>
        /// <param name="projId"></param>
        /// <returns></returns>
        Task<ConfigurationToSave> GetAllConfigurationsAsync(string projId);
        /// <summary>
        /// 获取指定配置的回滚版本。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ConfigurationToRollBack> GetRollBackConfigurationsAsync(string id);
        // 其他必要方法
    }
}
