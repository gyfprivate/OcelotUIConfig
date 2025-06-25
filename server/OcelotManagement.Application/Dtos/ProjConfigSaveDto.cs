using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos
{
    public class ProjConfigSaveDto
    {
        public string ProjId { get; set; }
        public string ConfigId { get; set; }
    }

    public class ProjRouteSaveDto
    {
        public string ProjId { get; set; }
        public List<string> RouteIds { get; set; }
    }
}
