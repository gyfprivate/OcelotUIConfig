using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Domain.Entities
{
    public class SyncProjInfo : Project
    {
        public ConfigCenter Config { get; set; }
        public List<Project> RouteProjs { get; set; }
    }
}
