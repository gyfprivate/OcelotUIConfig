using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos
{
    public class GlobalConfigurationDto
    {
        /// <summary>
        /// 主键Id 
        ///</summary>
        public string? Id { get; set; }

        public string? ProjId { get; set; }
        /// <summary>
        /// 基础地址 
        ///</summary>
        public string? BaseUrl { get; set; }
        /// <summary>
        /// 请求ID 
        ///</summary>
        public string? RequestIdKey { get; set; }
        /// <summary>
        /// Http协议（http,https） 
        ///</summary>
        public string? DownstreamScheme { get; set; }
        /// <summary>
        /// Http版本（1.0，1.1，2.0） 
        ///</summary>
        public string? DownstreamHttpVersion { get; set; }
        /// <summary>
        /// 负载均衡方式（LeastConnection，RoundRobin，NoLoadBalance） 
        ///</summary>
        public string? LoadbalancerType { get; set; }
        /// <summary>
        /// 负载均衡关键字 
        ///</summary>
        public string? LoadbalancerKey { get; set; }
        /// <summary>
        /// 负载均衡结束时间（ms） 
        ///</summary>
        public int? LoadbalancerExpiry { get; set; }
        /// <summary>
        /// 允许自定重定向 
        ///</summary>
        public bool? HttphandlerAllowautoredirect { get; set; }
        /// <summary>
        /// 使用Cookie容器 
        ///</summary>
        public bool? HttphandlerUsecookiecontainer { get; set; }
        /// <summary>
        /// 使用追踪 
        ///</summary>
        public bool? HttphandlerUsetracing { get; set; }
        /// <summary>
        /// 使用代理 
        ///</summary>
        public bool? HttphandlerUseproxy { get; set; }
        /// <summary>
        ///  
        ///</summary>
        public int? HttphandlerMaxconnectionsperserver { get; set; }
        /// <summary>
        /// 流量调控开启 
        ///</summary>
        public bool? QosEnabled { get; set; }
        /// <summary>
        /// 打开断路器之前允许的例外数量 
        ///</summary>
        public int? QosExceptionsallowedbeforebreaking { get; set; }
        /// <summary>
        /// 断路器复位之前打开的时间（ms） 
        ///</summary>
        public int? QosDurationofbreak { get; set; }
        /// <summary>
        /// 请求超时时间（ms） 
        ///</summary>
        public int? QosTimeoutvalue { get; set; }
        /// <summary>
        /// 客户Header 
        ///</summary>
        public string? RatelimitClientidheader { get; set; }
        /// <summary>
        /// 超过限制提示语 
        ///</summary>
        public string? RatelimitQuotaexceededmessage { get; set; }
        /// <summary>
        /// 计数前缀 
        ///</summary>
        public string? RatelimitRatelimitcounterprefix { get; set; }
        /// <summary>
        /// 包含X-Rate-Limit和Rety-After 
        ///</summary>
        public bool? RatelimitDisableratelimitheaders { get; set; }
        /// <summary>
        /// 超过限制Http状态码 
        ///</summary>
        public int? RatelimitHttpstatuscode { get; set; }
        /// <summary>
        /// Http协议（http,https） 
        ///</summary>
        public string? ServicediscoveryScheme { get; set; }
        /// <summary>
        ///  
        ///</summary>
        public string? ServicediscoveryHost { get; set; }
        /// <summary>
        ///  
        ///</summary>
        public int? ServicediscoveryPort { get; set; }
        /// <summary>
        ///  
        ///</summary>
        public string? ServicediscoveryType { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string? ServicediscoveryToken { get; set; }

        /// <summary>
        /// 配置key 
        ///</summary>
        public string? ServicediscoveryConfigurationkey { get; set; }

        /// <summary>
        /// 轮询间隔 
        ///</summary>
        public int? ServicediscoveryPollinginterval { get; set; }

        /// <summary>
        /// 命名空间 
        ///</summary>
        public string? ServicediscoveryNamespace { get; set; }

        /// <summary>
        /// 服务发现开启 
        ///</summary>
        public bool? ServiceDiscoveryEnabled { get; set; }

        /// <summary>
        /// 配置启动
        ///</summary>
        public bool Enabled { get; set; }
    }
}
