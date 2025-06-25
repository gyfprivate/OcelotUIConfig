using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Contracts.Interfaces
{
    public interface ISnowflake
    {
        string GetSnowID();
    }
}
