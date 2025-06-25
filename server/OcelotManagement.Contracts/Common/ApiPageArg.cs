using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Contracts.Common
{
    public class ApiPageArg
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
