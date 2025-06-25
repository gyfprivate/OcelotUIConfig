using Consul;
using OcelotManagement.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Infrastructure.Services
{
    // Consul服务实现
    public class ConsulService : IConsulService
    {


        public async Task WriteKvAsync(string host, int port, string dataCenter, string path, string value)
        {

            using ConsulClient client = new ConsulClient(c =>
                {
                    c.Address = new Uri($"http://{host}:{port}/");
                    c.Datacenter = dataCenter;
                });

            var kvPair = new KVPair(path)
            {
                Value = Encoding.UTF8.GetBytes(value)
            };

            var result = await client.KV.Put(kvPair);
            if (!result.Response)
            {
                throw new Exception($"Failed to write to Consul KV at key: {path}");
            }
        }

        public async Task<string> ReadKvAsync(string host, int port, string dataCenter, string path)
        {
            using ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri($"http://{host}:{port}/");
                c.Datacenter = dataCenter;
            });
            var queryResult = await client.KV.Get(path);
            if (queryResult.Response == null)
            {
                return null;
            }

            return Encoding.UTF8.GetString(queryResult.Response.Value);
        }
    }

}
