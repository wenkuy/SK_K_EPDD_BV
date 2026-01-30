using BaseProj;
using CommunityToolkit.Mvvm.Messaging;
using DataAnalasisProj.Model;
using Dragablz;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace DataAnalasisProj.View
{
    /// <summary>
    /// DataAnalysisWnd.xaml 的交互逻辑
    /// </summary>
    public partial class DataAnalysisWnd : Window
    {
        public ObservableCollection<HeaderedItemViewModel> ToolItems { get; }
        public DataAnalysisWnd()
        {
            InitializeComponent();
            ToolItems = new ObservableCollection<HeaderedItemViewModel>();
            // 正确绑定方式：直接赋值给 LayoutablzControl 的 ToolItems 属性
            DragLyout.FloatingItemsSource = ToolItems;
        }

        /// <summary>
        /// 添加图表Chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddChartButton_Click(object sender, RoutedEventArgs e)
        {
            ToolItems.Add(new HeaderedItemViewModel()
            {
                Header = $"图表 {ToolItems.Count + 1}",
                Content = new CartesianChart()
                {
                    // 关键设置：让图表自动拉伸填满父容器
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Background = Brushes.LightBlue, // 加背景色，能直观看到图表控件
                    Width = 400, // 给浮动面板一个基础宽高
                    Height = 300
                }
            });

          
           


        }

        private void AddSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            TabItem item = new TabItem();
            item.Header = "曲线";

            //新建一个Grid
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            Button comfirm = new Button() { Content = "确认", Height = 50, Width = 100 };
            comfirm.Click += ConfirmButtonClick;
            grid.Children.Add(comfirm);
            Grid.SetColumn(comfirm, 1);
            Grid.SetRow(comfirm, 1);

            item.Content = grid;
            TabCtrl.Items.Add(item);

            //选中该item，内容才会显示
            item.IsSelected = true;
        }

        private void DeleteSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            TabCtrl.Items.RemoveAt(TabCtrl.Items.Count - 1);
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            var kk  = DragControl.Items.CurrentItem as TabItem;
            if (null == DragControl.Items.CurrentItem as CartesianChart) return;

            CartesianChart chart = (CartesianChart)DragControl.Items.CurrentItem;

            var series2 = new ObservableCollection<ISeries>();
            var line = new LineSeries<ObservablePoint>{ Name = "温度",Values = GetDatas()};
            series2.Add(line);
            chart.SeriesSource = series2;
          


        }




        private ObservableCollection<ObservablePoint> GetDatas()
        {
            ObservableCollection<ObservablePoint> datas = new ObservableCollection<ObservablePoint>();
            Random random = new Random();
            for (int i = 0; i < 300; i++)
            {
                datas.Add(new ObservablePoint() { X = i, Y = random.Next(50, 100) });
            }
            return datas;
        }
    }
}
