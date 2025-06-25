using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos.Ocelot
{
    public class GlobalConfigurationDto
    {
        public ServiceDiscoveryProviderDto ServiceDiscoveryProvider { get; set; }

        public string RequestIdKey { get; set; }

        public GlobalRateLimitOptionDto RateLimitOptions { get; set; }
    }
}
