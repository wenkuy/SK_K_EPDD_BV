using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Repository
{
    /// <summary>
    /// 通用仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Add(T entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        bool Update(Expression<Func<T, bool>> expression, Expression<Func<Microsoft.EntityFrameworkCore.Query.SetPropertyCalls<T>, Microsoft.EntityFrameworkCore.Query.SetPropertyCalls<T>>> updateExpression);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 获取多个实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T[] GetAll(Expression<Func<T, bool>> expression);
    }
}
