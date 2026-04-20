using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Repository
{
    /// <summary>
    /// 通用仓储实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CSK_DBContext _dbContext;
        protected readonly ILogger _logger;

        public Repository(CSK_DBContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public bool Add(T entity)
        {
            if (entity == null)
            {
                _logger.LogWarning("尝试添加空实体");
                return false;
            }

            try
            {
                _dbContext.Add(entity);
                _dbContext.SaveChanges();
                _logger.LogInformation($"成功添加实体: {typeof(T).Name}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"添加实体失败: {typeof(T).Name}");
                return false;
            }
        }

        public bool Delete(Expression<Func<T, bool>> expression)
        {
            try
            {
                var result = _dbContext.Set<T>().Where(expression).ExecuteDelete();
                _logger.LogInformation($"成功删除 {result} 个实体: {typeof(T).Name}");
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"删除实体失败: {typeof(T).Name}");
                return false;
            }
        }

        public bool Update(Expression<Func<T, bool>> expression, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
        {
            try
            {
                var result = _dbContext.Set<T>().Where(expression).ExecuteUpdate(updateExpression);
                _logger.LogInformation($"成功更新 {result} 个实体: {typeof(T).Name}");
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"更新实体失败: {typeof(T).Name}");
                return false;
            }
        }

        public T GetSingle(Expression<Func<T, bool>> expression)
        {
            try
            {
                var entity = _dbContext.Set<T>().FirstOrDefault(expression);
                if (entity == null)
                {
                    _logger.LogInformation($"未找到实体: {typeof(T).Name}");
                }
                else
                {
                    _logger.LogInformation($"成功获取实体: {typeof(T).Name}");
                }
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取实体失败: {typeof(T).Name}");
                return null;
            }
        }

        public T[] GetAll(Expression<Func<T, bool>> expression)
        {
            try
            {
                var entities = _dbContext.Set<T>().Where(expression).ToArray();
                _logger.LogInformation($"成功获取 {entities.Length} 个实体: {typeof(T).Name}");
                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取实体列表失败: {typeof(T).Name}");
                return null;
            }
        }
    }
}