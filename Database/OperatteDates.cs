using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class OperationDate : IDBRecordPOperation
    {
        private readonly CSK_DBContext _dbc;
        public OperationDate(CSK_DBContext dbc)
        {
            _dbc = dbc;
        }
        public bool DBDeleteObj<T>(T t)
        {

            if (t == null)
            {
                return false; // Return false if the input object is null
            }

            try
            {
                _dbc.Remove(t); // Attempt to remove the entity
                _dbc.SaveChanges(); // Save changes to the database
                return true; // Return true if the operation succeeds
            }
            catch (Exception)
            {
                return false; // Return false if an exception occurs
            }

        }

        public bool DBGetObj<T>(Expression<Func<T, bool>> expression) where T : class
        {

            try
            {
                // Check if any object matching the expression exists in the database
                var result = _dbc.Set<T>().Any(expression);  //•	Set<T>() 是 DbContext 的一个方法，用于获取指定实体类型 T 的 
                return result; // Return true if found, false otherwise
            }
            catch (Exception)
            {
                return false; // Return false if an exception occurs
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
            catch (Exception)
            {
                return false; // Return false if an exception occurs
            }

        }
    }
}
