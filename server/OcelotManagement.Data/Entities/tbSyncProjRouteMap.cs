using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Data.Entities
{
    public class tbSyncProjRouteMap
    {
        /// <summary>
        /// 同步项目主键
        /// </summary>
        public string ProjId { get; set; }
        /// <summary>
        /// 路由主键
        /// </summary>
        public string RouteId { get; set; }
    }
}
