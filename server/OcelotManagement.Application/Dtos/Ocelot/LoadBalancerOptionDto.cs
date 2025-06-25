using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos.Ocelot
{
    //负载均衡
    public class LoadBalancerOptionDto
    {
        public string Type { get; set; }

        public string Key { get; set; }

        public int? Expiry { get; set; }
    }
}
