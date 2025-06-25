using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos.Ocelot
{
    public class GlobalRateLimitOptionDto
    {
        public bool? DisableRateLimitHeaders { get; set; }

        public string QuotaExceededMessage { get; set; }

        public int? HttpStatusCode { get; set; }

        public string ClientIdHeader { get; set; }

        public string Period { get; set; }
    }
}
