using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Domain.Entities
{
    public class SettingBackup
    {
        public string Id { get; set; }
        public DateTime BackupTime { get; set; }
        public string BackupContent { get; set; }
        public string Remark { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public ConfigCenter Consul { get; set; }
    }
}
