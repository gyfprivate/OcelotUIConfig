using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Data.Entities
{
    /// <summary>
    /// 项目表
    ///</summary>
    public class tbProject
    {
        /// <summary>
        /// 主键Id 
        ///</summary>
        public string Id { get; set; }
        /// <summary>
        /// 项目名称 
        ///</summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 排序字段 
        ///</summary>
        public int OrderIndex { get; set; }
        /// <summary>
        /// 启用 
        ///</summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 创建时间 
        ///</summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 编辑时间 
        ///</summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 项目类型
        /// 1 同步项目 包括concel中心，路由项目
        /// 2 路由项目 包括所有路由配置
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 配置id，对应tbConfigCenter表的主键Id
        /// </summary>
        public string ConfigId { get; set; }

    }
}

