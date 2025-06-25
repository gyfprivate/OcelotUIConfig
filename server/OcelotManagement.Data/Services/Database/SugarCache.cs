using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Infrastructure.Services.DatabaseCache
{
    internal class SugarCache : ICacheService
    {
        private MemoryCacheHelper cache = new MemoryCacheHelper();

        public void Add<V>(string key, V value)
        {
            Console.WriteLine($"Add cache {key}");
            //GlobalLog.Info($"Add cache {key}");
            cache.Set(key, value);
        }

        public void Add<V>(string key, V value, int cacheDurationInSeconds)
        {
            Console.WriteLine($"Add cache {key} in {cacheDurationInSeconds}");
            //GlobalLog.Info($"Add cache {key} in {cacheDurationInSeconds}");
            cache.Set(key, value, cacheDurationInSeconds);
        }

        public bool ContainsKey<V>(string key)
        {
            return cache.Exists(key);
        }

        public V Get<V>(string key)
        {
            return cache.Get<V>(key);
        }

        public IEnumerable<string> GetAllKey<V>()
        {
            return cache.GetCacheKeys();
        }

        public V GetOrCreate<V>(string cacheKey, Func<V> create, int cacheDurationInSeconds = int.MaxValue)
        {
            //Console.WriteLine("不使用 cache");
            //return create();

            Console.WriteLine($"GetOrCreate cache {cacheKey}");
            //GlobalLog.Info($"GetOrCreate cache {cacheKey}");
            if (cache.Exists(cacheKey))
            {
                return cache.Get<V>(cacheKey);
            }
            else
            {
                var result = create();
                cache.Set(cacheKey, result, cacheDurationInSeconds);
                return result;
            }
        }

        public void Remove<V>(string key)
        {
            Console.WriteLine($"Remove cache {key}");
            //GlobalLog.Info($"Remove cache {key}");
            cache.Remove(key);
        }
    }
}
