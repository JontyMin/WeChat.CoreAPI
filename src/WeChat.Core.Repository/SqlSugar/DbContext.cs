using System;
using System.Linq;
using SqlSugar;
using WeChat.Core.Common.Helper;
using WeChat.Core.Model.EntityModel;

namespace WeChat.Core.Repository.SqlSugar
{
    public class DbContext<T> where T : class, new()
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString =BaseDBConfig.ConnectionString,
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute, //从特性读取主键和自增列信息
                IsAutoCloseConnection = true, //开启自动释放模式
            });
            
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                                  Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
        }

        public SqlSugarClient Db;
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来操作当前表的数据
        
    }
}