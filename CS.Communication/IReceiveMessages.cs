using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Communication
{
    public interface IReceiveMessages
    {
        public void DelMessage(byte[] bytes);
    }
}
