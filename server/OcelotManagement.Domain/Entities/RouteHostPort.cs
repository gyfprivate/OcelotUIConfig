using System;
using System.Collections.Generic;
using System.Linq;

namespace  OcelotManagement.Domain.Entities
{
    /// <summary>
    /// 路由地址配置表
    ///</summary>
    public class RouteHostPort
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
    }
}
