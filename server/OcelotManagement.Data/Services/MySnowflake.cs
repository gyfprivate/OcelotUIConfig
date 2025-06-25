using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Data.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Data.Services
{
    /// <summary>
    /// 雪花id初始化
    ///     //https://www.donet5.com/home/doc?masterId=1&typeId=2561
    /// </summary>
    public class MySnowflake : ISnowflake
    {

        public static void AddSnowflakeOption(OptionsSnowFlake op)
        {
            SnowFlakeSingle.WorkId = op.worker == 0 ? 1 : op.worker;
            SnowFlakeSingle.DatacenterId = op.datacenter == 0 ? 1 : op.datacenter;
            var ran = new Random();
            StaticConfig.CustomSnowFlakeTimeErrorFunc = () =>
            {
                return ran.Next(16, 18);//出现时间回退使用临时算法插入
            };
        }

        /// <summary>
        /// 获取唯一雪花id
        /// </summary>
        /// <returns></returns>
        public string GetSnowID()
        {
            return SnowFlakeSingle.Instance.NextId().ToString("X16");
        }
    }
}
