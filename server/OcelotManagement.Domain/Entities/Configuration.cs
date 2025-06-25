using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Domain.Entities
{
    /// <summary>
    /// 保存的配置实体类
    /// </summary>
    public class ConfigurationToSave
    {
        /// <summary>
        /// 路由配置
        /// </summary>
        public List<Route> Routes;
        /// <summary>
        /// 当前配置 中心信息
        /// </summary>
        public ConfigCenter ConfigCenter;
        /// <summary>
        /// 全局配置
        /// </summary>
        public GlobalConfiguration Global;
    }
}
