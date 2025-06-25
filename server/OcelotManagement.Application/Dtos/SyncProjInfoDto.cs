using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos
{

    /// <summary>
    /// 项目同步信息数据传输对象
    /// </summary>
    public class SyncProjInfoDto : ProjDto
    {
        /// <summary>
        /// 配置中心信息数据传输对象
        /// </summary>
        public ConfigCenterDto ConfigCenter { get; set; }

        /// <summary>
        /// 同步Ocelot 项目列表
        /// </summary>
        public List<ProjDto> RouteProjs{ get; set; }
    }
}
