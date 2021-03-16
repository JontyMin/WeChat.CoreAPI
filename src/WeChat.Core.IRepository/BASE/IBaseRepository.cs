using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WeChat.Core.IRepository.BASE
{
    public interface IBaseRepository<T> where T:class
    {
        #region Transaction

        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTran();
        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTran();
        /// <summary>
        /// 事务回滚
        /// </summary>
        void RollbackTran();

        #endregion

        #region Create

        /// <summary>
        /// 实体新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(T model);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<int> Add(List<T> list);


        #endregion

        #region Delete

        /// <summary>
        /// Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteById(object id);

        /// <summary>
        /// 实体删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Delete(T model);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteByIds(object[] ids);

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<int> Deleteable(Expression<Func<T, bool>> whereExpression);
        #endregion

        #region Update

        /// <summary>
        /// 实体更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Update(T model);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<int> Update(List<T> list);

        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        Task<bool> Update(T model, string strWhere);

        #endregion

        #region Query

        /// <summary>
        /// Id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> QueryById(object id);

        /// <summary>
        /// 多Id查询
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<T>> QueryByIds(object[] ids);

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<T>> Query();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        Task<List<T>> Query(string strWhere);

        /// <summary>
        /// 条件表达式查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<List<T>> Query(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 原生sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<List<T>> Queryable(string sql);

        #endregion

    }
}