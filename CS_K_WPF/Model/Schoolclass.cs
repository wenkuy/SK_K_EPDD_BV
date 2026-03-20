using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CS_K_WPF.Model
{
    public class Schoolclass
    {
        public string Name { get; set; }

        public int StudentId { get; set; }

        public int Age { get; set; }

        public ICommand CMD { get; set; }
    }
}
