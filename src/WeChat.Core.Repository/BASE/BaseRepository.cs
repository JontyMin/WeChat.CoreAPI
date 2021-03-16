using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Records.NotUsed;
using WeChat.Core.IRepository.BASE;
using WeChat.Core.Repository.SqlSugar;

namespace WeChat.Core.Repository.BASE
{
    public class BaseRepository<T>:DbContext<T>,IBaseRepository<T>where T:class,new()
    {
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            Db.Ado.BeginTran();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTran()
        {
            Db.Ado.CommitTran();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTran()
        {
            Db.Ado.RollbackTran();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(T model)
        {
            var insert = Db.Insertable(model);
            return await insert.ExecuteReturnIdentityAsync();
        }

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> Add(List<T> list)
        {
            return await Db.Insertable(list.ToArray()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除指定Id数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            return await Db.Deleteable<T>(id).ExecuteCommandHasChangeAsync();
        }
        
        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Delete(T model)
        {
            return await Db.Deleteable(model).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await Db.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<int> Deleteable(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Deleteable<T>().Where(whereExpression).ExecuteCommandAsync();
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(T model)
        {
            return await Db.Updateable(model).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> Update(List<T> list)
        {
            return await Db.Updateable(list.ToArray()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public async Task<bool> Update(T model, string strWhere)
        {
            return await Db.Updateable(model).Where(strWhere).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 根据Id查询一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> QueryById(object id)
        {
            return await Db.Queryable<T>().In(id).SingleAsync();
        }

        /// <summary>
        /// 根据Id集合查询数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryByIds(object[] ids)
        {
            return await Db.Queryable<T>().In(ids).ToListAsync();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> Query()
        {
            return await Db.Queryable<T>().ToListAsync();
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public async Task<List<T>> Query(string strWhere)
        {
            return await Db.Queryable<T>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
        }

        /// <summary>
        /// 条件表达式查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<List<T>> Query(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Queryable<T>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        /// <summary>
        /// 原生sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<List<T>> Queryable(string sql)
        {
            return await Db.SqlQueryable<T>(sql).ToListAsync();
        }
    }
}