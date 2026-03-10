using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.Model
{
    public  class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TestClass> Children { get; set; }
    }
}
