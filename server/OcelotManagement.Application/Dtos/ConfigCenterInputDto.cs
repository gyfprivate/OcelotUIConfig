using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos
{
    public class ConfigCenterInputDto
    {
        /// <summary>
        /// IP
        ///</summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口
        ///</summary>
        public int Port { get; set; }
        /// <summary>
        /// 备注
        ///</summary>
        public string Remark { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 所属位置
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 是否启用
        ///</summary>
        public bool Enabled { get; set; }
    }
}
