using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database
{
    /// <summary>
    /// 不可鞥每个类都写一套查询，所以搞了泛型操作类，实现对数据库的增删查改
    /// </summary>
    public class OperationDate : IDBRecordPOperation
    {
        
        private readonly CSK_DBContext _dbc; //DI注入
        private ILogger<OperationDate> _logger;
        public OperationDate(CSK_DBContext dbc , ILogger<OperationDate> logger) //Logger注入，记录日志
        {
            _dbc = dbc;
            _logger = logger;
        }

        public bool DBDeleteObj<T>(Expression<Func<T, bool>> expression) where T : class
        {

            try
            {
                _dbc.Set<T>().Where(expression).ExecuteDelete();//查出符合条件的数据，批量删除
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false ;
            }
        }


        public T[] DBGetObjs<T>(Expression<Func<T, bool>> expression) where T : class
        {
            try
            {
                return _dbc.Set<T>().Where(expression).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public T DBGetSingleObj<T>(Expression<Func<T, bool>> expression) where T : class
        {

            try
            {
                // Attempt to retrieve a single entity matching the expression
                return _dbc.Set<T>().FirstOrDefault(expression);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null; // Return false if an exception occurs
            }

        }

        public  bool  DBSaveObj<T>(T t)
        {

            if (t == null)
            {
                return false; // Return false if the input object is null
            }

            try
            {
                _dbc.Add(t); // Add the entity to the database context
                _dbc.SaveChanges(); // Save changes to the database
                return true; // Return true if the operation succeeds
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false; // Return false if an exception occurs
            }
        }

        /// <summary>
        /// 编辑对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public bool EditObj<T>(Expression<Func<T, bool>> expression, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression) where T : class
        {
            // 1. SetPropertyCalls 就是EFCore定义的一个类，记录要更新的属性和值
            // 2. Func<SetPropertyCalls<T>, SetPropertyCalls<T>> 是为了实现链式调用
            try
            {
                _dbc.Set<T>().Where(expression).ExecuteUpdate(updateExpression);//查出符合条件的数据，批量更新
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }
    }
}
