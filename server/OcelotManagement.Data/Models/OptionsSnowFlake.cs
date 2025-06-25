using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Data.Models
{
    /// <summary>
    /// 雪花id配置
    /// </summary>
    public class OptionsSnowFlake
    {
        /// <summary>
        /// 机器id
        /// </summary>
        public int worker { get; set; }
        /// <summary>
        /// 服务器机房id
        /// </summary>
        public int datacenter { get; set; }
    }

}
