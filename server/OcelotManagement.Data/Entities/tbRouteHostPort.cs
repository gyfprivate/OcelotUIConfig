using System;
using System.Collections.Generic;
using System.Linq;

namespace OcelotManagement.Data.Entities
{
    /// <summary>
    /// 路由地址配置表
    ///</summary>
    public class tbRouteHostPort
    {
        /// <summary>
        /// 主键Id 
        ///</summary>
        public string Id { get; set; }
        /// <summary>
        ///  
        ///</summary>
        public string Host { get; set; }
        /// <summary>
        ///  
        ///</summary>
        public int Port { get; set; }
        /// <summary>
        /// 路由Id 
        ///</summary>
        public string RouteId { get; set; }
    }
}
