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
        public bool DBDeleteObj<T>(T t);
        public bool DBGetObj<T>(Expression<Func<T, bool>> expression) where T : class;
    }
       
}
