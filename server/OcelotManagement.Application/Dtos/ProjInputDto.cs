using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos
{
    public class ProjInputDto
    {
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
    }
}
