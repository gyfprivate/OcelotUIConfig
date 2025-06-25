using System;
using System.Collections.Generic;
using System.Linq;

namespace Ocelot.Admin.Api.Core
{
    /// <summary>
    /// 配置备份
    ///</summary>
    public class SettingBak
    {
        /// <summary>
        /// 主键Id 
        ///</summary>
        public string Id { get; set; }

        /// <summary>
        /// 备份时间
        ///</summary>
        public DateTime BakTime { get; set; }

        /// <summary>
        /// 备份内容
        ///</summary>
        public string BakJson { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// ConsulKey
        /// </summary>
        public string ConsulKey { get; set; }

        /// <summary>
        /// ConsulDc
        /// </summary>
        public string ConsulDc { get; set; }
    }
}
