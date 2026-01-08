using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public interface IDBRecordPOperation
    {
        public bool DBSaveObj<T>(T t);
        public bool DBDeleteObj<T>(Expression<Func<T, bool>> expression) where T : class;
        public bool EditObj<T>(Expression<Func<T, bool>> expression, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression) where T : class;
        public T DBGetSingleObj<T>(Expression<Func<T, bool>> expression) where T : class;
        public T[] DBGetObjs<T>(Expression<Func<T, bool>> expression) where T : class;
    }
       
}
