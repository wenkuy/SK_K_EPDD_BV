using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalasisProj.View
{
    public class FloatingPanelViewModel : ObservableObject
    {
        public string Name { get; set; }

        public object Content { get; set; }

    }
}
