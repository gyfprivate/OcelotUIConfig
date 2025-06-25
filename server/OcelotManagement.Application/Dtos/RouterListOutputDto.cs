using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos
{

    public class RouterListOutputDto
    {
        public string Id { get; set; }
        public string UpstreamPathTemplate { get; set; }
        public string DownstreamPathTemplate { get; set; }
        public bool Enabled { get; set; }
    }
}
