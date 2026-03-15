using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.Model
{
    public class TreeViewYear
    {
        public string Year { get; set; }
        public List<TreeViewMonth> Months { get; set; }
    }

    public class TreeViewMonth
    {
        public string Month { get; set; }
        public List<TreeViewDay> Days { get; set; } 
    }

    public class TreeViewDay
    {
        public string Day { get; set; }
        public List<TreeViewLight> Files { get; set; } 
    }

}
