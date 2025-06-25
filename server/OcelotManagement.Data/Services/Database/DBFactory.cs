using Microsoft.Extensions.Options;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcelotManagement.Infrastructure.Services.DatabaseCache;

namespace OcelotManagement.Data.Services.Database
{
    public class DBFactory
    {
        private SqlSugarClient db;
        private IOptionsMonitor<ConnectionConfig> op;
        public DBFactory(IOptionsMonitor<ConnectionConfig> op)
        {
            this.op = op;
        }
        public SqlSugarClient CreateDB()
        {
            if (db == null)
            {
                //通过配置获取 ConnectionConfig
                var value = op.CurrentValue;
                value.IsAutoCloseConnection = true;
                value.MoreSettings = new ConnMoreSettings() { IsAutoRemoveDataCache = false };
                value.ConfigureExternalServices = GetConfigExt();

                db = new SqlSugarClient(value
#if TEST_SQL
                   , d =>
                   {

                       d.Aop.OnLogExecuting = (sql, pars) =>
                   Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                       d.Aop.OnError = (exp) =>//SQL报错
                   {
                       //获取原生SQL推荐 5.1.4.63  性能OK
                       Console.WriteLine(

                       UtilMethods.GetNativeSql(exp.Sql, exp.Parametres as SugarParameter[]));

                       //获取无参数SQL对性能有影响，特别大的SQL参数多的，调试使用
                       //UtilMethods.GetSqlString(DbType.SqlServer,exp.sql,exp.parameters)           
                   };
                   }
#endif
               );
                //var db = new SqlSugarClient(new ConnectionConfig()
                //{
                //    DbType = DbType.SqlServer,
                //    ConnectionString = "Data Source=192.168.0.78;Initial Catalog=comsaas;User ID=sa;Password=123456",
                //    IsAutoCloseConnection = true,

                //    MoreSettings = new ConnMoreSettings()
                //    {
                //        IsAutoRemoveDataCache = false
                //    },
                //    ConfigureExternalServices = GetConfigExt()
                //});
            }
            return db;

            //return CreateDB_Sqlite_IMS("authentication.db");
        }
        private static ICacheService myCache = new SugarCache();

        protected ConfigureExternalServices GetConfigExt()
        {
            return new ConfigureExternalServices()
            {
                DataInfoCacheService = myCache,//二级缓存
                EntityNameService = (property, e) =>
                {
                    var attributes = property.GetCustomAttributes(true);//get all attributes
                    foreach (var item in attributes) /*tablename*/
                    {
                        if (item is TableAttribute ee)
                        {
                            e.DbTableName = ee.Name;
                        }
                    }
                },
                EntityService = (property, column) =>
                {
                    var attributes = property.GetCustomAttributes(true);//get all attributes               
                    if (attributes.Any(it => it is KeyAttribute))// by attribute set primarykey  设置了key 为自增主键
                    {
                        column.IsPrimarykey = true; //有哪些特性可以看 1.2 特性明细
                        column.IsIdentity = true;
                    }
                    if (column.PropertyName.ToLower() == "id") //是id的设为主键 名字为id 为主键
                    {
                        column.IsPrimarykey = true;
                        if (column.PropertyInfo.PropertyType == typeof(int) || column.PropertyInfo.PropertyType == typeof(long)) //是id并且是int的是自增
                        {
                            //类型为int 或 long 为自增
                            column.IsIdentity = true;
                        }
                    }
                    if (!column.IsPrimarykey) //主键一直不能为空
                    {
                        if (IsNullableType(column.PropertyInfo.PropertyType))   //可空
                        {
                            column.IsNullable = true;
                        }

                        if (column.PropertyInfo.PropertyType == typeof(string)) //string 为可空
                        {
                            column.IsNullable = true;
                        }
                    }

                    //decimal 小数位数
                    if (column.PropertyInfo.PropertyType == typeof(decimal))
                    {
                        column.DataType = "decimal(18,4)";
                    }

                    if (property.GetAccessors().Any(o => o.IsVirtual)) //导航查询
                    {
                        column.IsIgnore = true;//跳过
                    }
                    if (attributes.Any(it => it is NotMappedAttribute))
                    {
                        column.IsIgnore = true; //跳过
                    }

                    ExtSetConfig(property, column);

                    //column.IfTable<OperationLog2>()
                    //.UpdateProperty(o => o.Rem, o =>
                    //{
                    //    o.IsNullable = true;
                    //    o.Length = 4096;
                    //});


                },
            };
        }
        /// <summary>
        /// 额外配置
        /// </summary>
        /// <param name="property"></param>
        /// <param name="column"></param>
        protected virtual void ExtSetConfig(System.Reflection.PropertyInfo property, EntityColumnInfo column)
        {
        }
        //二级缓存
        static bool IsNullableType(Type type)
        {
            if (type.IsGenericType)
            {
                var definition = type.GetGenericTypeDefinition();

                if (definition != null && definition == typeof(Nullable<>))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
