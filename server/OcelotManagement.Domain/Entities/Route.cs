using System;
using System.Collections.Generic;
using System.Linq;

namespace OcelotManagement.Domain.Entities
{

    public class ListRoute
    {
        public string Id { get; set; }
        public string UpstreamPathTemplate { get; set; }
        public string DownstreamPathTemplate { get; set; }
        public bool Enabled { get; set; }
    }
    /// <summary>
    /// 路由表
    ///</summary>
    public class Route
    {
        public List<RouteHostPort>? Hosts { get; set; }
        /// <summary>
        /// 主键ID 
        ///</summary>
        public string Id { get; set; }

        /// <summary>
        /// 项目Id 
        ///</summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// 下游的路由模板，即真实处理请求的路径模板 
        ///</summary>
        public string DownstreamPathTemplate { get; set; }

        /// <summary>
        /// 上游请求的模板，即用户真实请求的链接 
        ///</summary>
        public string UpstreamPathTemplate { get; set; }

        /// <summary>
        /// 上游请求的http方法（数组：GET、POST、PUT） 
        ///</summary>
        public string UpstreamHttpMethod { get; set; }

        /// <summary>
        /// 下游请求的http方法（数组：GET、POST、PUT） 
        ///</summary>
        public string DownstreamHttpMethod { get; set; }

        /// <summary>
        ///  下游Http版本 
        ///</summary>
        public string DownstreamHttpVersion { get; set; }

        /// <summary>
        ///  请求Id 
        ///</summary>
        public string RequestIdKey { get; set; }

        /// <summary>
        ///  开启上下游路由模板大小写匹配 
        ///</summary>
        public bool? RouteIsCaseSensitive { get; set; }

        /// <summary>
        ///  服务名 
        ///</summary>
        public string ServiceName { get; set; }

        /// <summary>
        ///  服务命名空间 
        ///</summary>
        public string ServiceNamespace { get; set; }

        /// <summary>
        /// 请求的方式，如：http,htttps 
        ///</summary>
        public string DownstreamScheme { get; set; }

        /// <summary>
        /// 请求缓存过期时间（需使用Ocelot.Cache.CacheManager） 
        ///</summary>
        public int? CacheTtlseconds { get; set; }

        /// <summary>
        /// 缓存区域（需使用Ocelot.Cache.CacheManager） 
        ///</summary>
        public string CacheRegion { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public int? QosExceptionsallowedbeforebreaking { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public int? QosDurationofbreak { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public int? QosTimeoutvalue { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string LoadbalancerType { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string LoadbalancerKey { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public int? LoadbalancerExpiry { get; set; }

        /// <summary>
        /// 是否启用流量限制
        ///</summary>
        public bool? RatelimitEnableratelimiting { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string RatelimitPeriod { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public decimal? RatelimitPeriodtimespan { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public long? RatelimitLimit { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string RatelimitWhitelist { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string AuthenticationAuthenticationproviderkey { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string AuthenticationAllowedscopes { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public bool? HttphandlerAllowautoredirect { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public bool? HttphandlerUsecookiecontainer { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public bool? HttphandlerUsetracing { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public bool? HttphandlerUseproxy { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public int? HttphandlerMaxconnectionsperserver { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string UpstreamHost { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string Key { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string DelegatingHandlers { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public int? Priority { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// 评估危险服务验证 
        ///</summary>
        public bool? DangerousAcceptAnyServerCertificateValidator { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string SecurityIpallowedlist { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string SecurityIpblockedlist { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public bool Enabled { get; set; }
    }
}
