using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Contracts.Interfaces
{
    // Consul服务接口
    public interface IConsulService
    {
        Task WriteKvAsync(string host, int port, string dataCenter, string path, string value);
        Task<string> ReadKvAsync(string host, int port, string dataCenter, string path);
        // 其他必要方法
    }

}
