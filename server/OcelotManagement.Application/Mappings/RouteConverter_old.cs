using OcelotManagement.Application.Dtos.Ocelot;
using OcelotManagement.Domain.Entities;
using System.ComponentModel;
using System.Globalization;

namespace OcelotManagement.Application.Mappings
{
    public class RouteConverter_old : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(Route);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            var concreteValue = (Route)value;
            var result = new ConfigRouteDto();
            {
                // 认证选项
                if (string.IsNullOrEmpty(concreteValue.AuthenticationAuthenticationproviderkey) || string.IsNullOrEmpty(concreteValue.AuthenticationAllowedscopes))
                    result.AuthenticationOptions = null;
                else
                {
                    result.AuthenticationOptions = new AuthenticationOptionDto();
                    result.AuthenticationOptions.AuthenticationProviderKey = concreteValue.AuthenticationAuthenticationproviderkey;
                    result.AuthenticationOptions.AllowedScopes = concreteValue.AuthenticationAllowedscopes?.Split(',').ToList();
                }

                // 限流选项
                if (concreteValue.RatelimitEnableratelimiting == true
                    && concreteValue.RatelimitLimit != null
                    && concreteValue.RatelimitPeriod != null
                    && concreteValue.RatelimitPeriodtimespan != null
                    && (!string.IsNullOrEmpty(concreteValue.RatelimitWhitelist))
                )
                {
                    result.RateLimitOptions = new RateLimitOptionDto()
                    {
                        Limit = concreteValue.RatelimitLimit,
                        ClientWhitelist = concreteValue.RatelimitWhitelist?.Split(',').ToList(),
                        Period = concreteValue.RatelimitPeriod,
                        EnableRateLimiting = concreteValue.RatelimitEnableratelimiting,
                        PeriodTimespan = concreteValue.RatelimitPeriodtimespan
                    };
                }

                // 委托处理程序
                result.DelegatingHandlers = concreteValue.DelegatingHandlers?.Split(',').ToList();

                // 下游主机和端口
                if (concreteValue.Hosts != null && concreteValue.Hosts.Count > 0)
                {
                    result.DownstreamHostAndPorts = concreteValue.Hosts.ConvertAll(o => new DownstreamHostAndPortDto() { Host = o.Host, Port = o.Port });
                }

                // 下游 HTTP 方法和路径模板
                result.DownstreamHttpMethod = concreteValue.DownstreamHttpMethod;
                result.DownstreamPathTemplate = concreteValue.DownstreamPathTemplate;
                result.DownstreamScheme = concreteValue.DownstreamScheme;

                // 文件缓存选项
                if (concreteValue.CacheTtlseconds != null && !(string.IsNullOrWhiteSpace(concreteValue.CacheRegion)))
                {
                    result.FileCacheOptions = new FileCacheOptionDto() { TtlSeconds = concreteValue.CacheTtlseconds, Region = concreteValue.CacheRegion };
                }

                // 上游路径模板
                result.UpstreamPathTemplate = concreteValue.UpstreamPathTemplate;

                // 上游 HTTP 方法
                result.UpstreamHttpMethod = concreteValue.UpstreamHttpMethod?.Split(',').ToList();

                // 请求 ID 键
                result.RequestIdKey = concreteValue.RequestIdKey;

                // 路由大小写敏感
                result.RouteIsCaseSensitive = concreteValue.RouteIsCaseSensitive;

                // 服务名
                result.ServiceName = concreteValue.ServiceName;

                // 服务质量选项
                if (concreteValue.QosExceptionsallowedbeforebreaking != null || concreteValue.QosDurationofbreak != null || concreteValue.QosTimeoutvalue != null)
                {
                    result.QoSOptions = new QoSOptionDto()
                    {
                        ExceptionsAllowedBeforeBreaking = concreteValue.QosExceptionsallowedbeforebreaking,
                        DurationOfBreak = concreteValue.QosDurationofbreak,
                        TimeoutValue = concreteValue.QosTimeoutvalue
                    };
                }

                // 负载均衡选项
                if (!string.IsNullOrEmpty(concreteValue.LoadbalancerType) || !string.IsNullOrEmpty(concreteValue.LoadbalancerKey) || concreteValue.LoadbalancerExpiry != null)
                {
                    result.LoadBalancerOptions = new LoadBalancerOptionDto()
                    {
                        Type = concreteValue.LoadbalancerType,
                        Key = concreteValue.LoadbalancerKey,
                        Expiry = concreteValue.LoadbalancerExpiry
                    };
                }

                // 是否使用服务发现
                result.UseServiceDiscovery = concreteValue.ServiceName != null;

                // 危险证书验证
                result.DangerousAcceptAnyServerCertificateValidator = concreteValue.DangerousAcceptAnyServerCertificateValidator;

                // HTTP 处理程序选项
                if (concreteValue.HttphandlerAllowautoredirect != null || concreteValue.HttphandlerUsecookiecontainer != null || concreteValue.HttphandlerUsetracing != null || concreteValue.HttphandlerUseproxy != null || concreteValue.HttphandlerMaxconnectionsperserver != null)
                {
                    result.HttpHandlerOptions = new HttpHandlerOptionDto()
                    {
                        AllowAutoRedirect = concreteValue.HttphandlerAllowautoredirect,
                        UseCookieContainer = concreteValue.HttphandlerUsecookiecontainer,
                        UseTracing = concreteValue.HttphandlerUsetracing,
                        UseProxy = concreteValue.HttphandlerUseproxy,
                        MaxConnectionsPerServer = concreteValue.HttphandlerMaxconnectionsperserver
                    };
                }
            }


            return result;
        }
    }
}
