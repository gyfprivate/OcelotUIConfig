using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos
{
    // DTO定义 (应用层)
    public class SettingBackupDto
    {
        public string Id { get; set; }
        public DateTime BackupTime { get; set; }
        public string BackupContent { get; set; }
        public string Remark { get; set; }
        public string ProjectName { get; set; }
        public ConsulCenterDto Consul { get; set; }
    }
}
