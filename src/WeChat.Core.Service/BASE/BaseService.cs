using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService.BASE;

namespace WeChat.Core.Service.BASE
{
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        private readonly IBaseRepository<T> _base;

        public BaseService(IBaseRepository<T>baseRepository)
        {
            _base = baseRepository;
        }
        
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            _base.BeginTran();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTran()
        {
            _base.CommitTran();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTran()
        {
            _base.RollbackTran();
        }
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(T model)
        {
            return await _base.Add(model);
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> Add(List<T> list)
        {
            return await _base.Add(list);
        }
        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            return await _base.DeleteById(id);
        }
        /// <summary>
        /// 实体删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Delete(T model)
        {
            return await _base.Delete(model);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await _base.DeleteByIds(ids);
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<int> Deleteable(Expression<Func<T, bool>> whereExpression)
        {
            return await _base.Deleteable(whereExpression);
        }

        /// <summary>
        /// 实体更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(T model)
        {
            return await _base.Update(model);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> Update(List<T> list)
        {
            return await _base.Update(list);
        }
        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public async Task<bool> Update(T model, string strWhere)
        {
            return await _base.Update(model, strWhere);
        }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> QueryById(object id)
        {
            return await _base.QueryById(id);
        }
        /// <summary>
        /// 根据Id集合查询
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryByIds(object[] ids)
        {
            return await _base.QueryByIds(ids);
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> Query()
        {
            return await _base.Query();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public async Task<List<T>> Query(string strWhere)
        {
            return await _base.Query(strWhere);
        }
        /// <summary>
        /// 表达式查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<List<T>> Query(Expression<Func<T, bool>> whereExpression)
        {
            return await _base.Query(whereExpression);
        }

        /// <summary>
        /// 原生sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<List<T>> Queryable(string sql)
        {
            return await _base.Queryable(sql);
        }

    }
}