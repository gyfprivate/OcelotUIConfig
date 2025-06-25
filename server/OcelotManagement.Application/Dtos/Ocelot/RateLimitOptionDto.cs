using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos.Ocelot
{
    public class RateLimitOptionDto
    {
        /// <summary>
        /// 限流白名单
        ///</summary>
        public List<string> ClientWhitelist { get; set; }

        /// <summary>
        /// 是否启用流量限制
        ///</summary>
        public bool? EnableRateLimiting { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string Period { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public decimal? PeriodTimespan { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public long? Limit { get; set; }

    }
}
