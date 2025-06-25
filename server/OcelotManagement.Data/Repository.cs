using Microsoft.Extensions.Options;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OcelotManagement.Infrastructure.Services.DatabaseCache;
using OcelotManagement.Data.Services.Database;

namespace OcelotManagement.Data
{
    public class Repository<T> : SimpleClient<T> where T : class, new()
    {
        public Repository(DBFactory f)
        {
            Context = f.CreateDB();
        }

        /// <summary>
        /// 扩展方法，自带方法不能满足的时候可以添加新方法
        /// </summary>
        /// <returns></returns>
        public List<T> CommQuery(string json)
        {
            //base.Context.Queryable<T>().ToList();可以拿到SqlSugarClient 做复杂操作
            return null;
        }

        /// <summary>
        /// 扩展方法，自带方法不能满足的时候可以添加新方法
        /// 无主键 更新
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columns">指定一个条件，当然支持多个 new {it.id,it.name}</param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdateAsync(T data, Expression<Func<T, object>> columns = null)
        {
            return await Context.Storageable(data).WhereColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 扩展方法，自带方法不能满足的时候可以添加新方法
        /// 无主键 更新
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columns">指定一个条件，当然支持多个 new {it.id,it.name}</param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdateAsync(List<T> data, Expression<Func<T, object>> columns = null)
        {
            return await Context.Storageable(data).WhereColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="action">事件</param>
        /// <param name="efunc">出错处理</param>
        public async Task<bool> TranAsync(Func<Task> action, Action<Exception> efunc)
        {
            try
            {
                await AsTenant().BeginTranAsync();
                await action();
                await AsTenant().CommitTranAsync();
                return true;
            }
            catch (Exception ex)
            {
                await AsTenant().RollbackTranAsync();
                efunc?.Invoke(ex);
                return false;
            }
        }
    }
}