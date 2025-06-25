using System;
using System.Collections.Generic;
using System.Linq;

namespace OcelotManagement.Data.Entities
{
    /// <summary>
    /// 配置备份
    ///</summary>
    public class tbSettingBak
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

        public string ProjId { get; set; }
    }
}
