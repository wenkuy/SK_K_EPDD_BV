using LiveChartsCore.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalasisProj.Model
{
    /// <summary>
    /// 自定义
    /// </summary>
    internal class ChartData
    {
        public string SeriesName { get; set; }
        public ObservableCollection<ObservablePoint> SeriesPoints { get; set; }
    }
}
