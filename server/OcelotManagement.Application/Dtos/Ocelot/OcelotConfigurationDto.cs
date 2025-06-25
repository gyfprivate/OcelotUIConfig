using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos.Ocelot
{
    /// <summary>
    /// ocelot配置
    /// </summary>
    public class OcelotConfigurationDto
    {
        public List<ConfigRouteDto> Routes { get; set; }

        public GlobalConfigurationDto GlobalConfiguration { get; set; }
    }


    public class DownstreamHostAndPortDto
    {
        public string Host { get; set; }

        public int Port { get; set; }
    }

    /// <summary>
    /// 服务质量
    /// </summary>
    public class QoSOptionDto
    {
        public int? ExceptionsAllowedBeforeBreaking { get; set; }

        public int? DurationOfBreak { get; set; }

        public int? TimeoutValue { get; set; }
    }

    public class HttpHandlerOptionDto
    {
        public bool? AllowAutoRedirect { get; set; }

        public bool? UseCookieContainer { get; set; }

        public bool? UseTracing { get; set; }

        public bool? UseProxy { get; set; }

        public int? MaxConnectionsPerServer { get; set; }
    }

    public class AuthenticationOptionDto
    {
        public string AuthenticationProviderKey { get; set; }

        public List<string> AllowedScopes { get; set; }
    }

    public class ServiceDiscoveryProviderDto
    {
        public string Host { get; set; }

        public int? Port { get; set; }

        public string? Type { get; set; }
        public int? PollingInterval { get; set; }
    }
}
