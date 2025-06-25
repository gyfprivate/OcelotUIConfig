using System;
using System.Collections.Generic;
using System.Linq;

namespace OcelotManagement.Data.Entities
{
    /// <summary>
    /// 路由属性表
    ///</summary>
    public class tbRouteProperty
    {
        /// <summary>
        /// 主键Id 
        ///</summary>
        public string Id { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string Key { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string Value { get; set; }

        /// <summary>
        /// 路由Id 
        ///</summary>
        public int RouteId { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public int Type { get; set; }
    }
}
