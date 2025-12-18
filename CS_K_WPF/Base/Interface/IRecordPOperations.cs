using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.Base.Interface
{
    public  class IDBRecordPOperation<T>
    {
        public bool DBSave<T>(T t)
        {
            return true;
        }

        public bool DBDelete<T>(T t)
        {
            return true;
        }

        public bool DBGet<T>(T t)
        {
            return true;
        }
    }
}
