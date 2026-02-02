using Dragablz;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

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
                },
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
            //var kk = DragLyout.FloatingItems.CurrentItem;
            var kk = DragLyout.FloatingItems[0];
            var activeFloatingItem = kk as HeaderedItemViewModel;
            if (activeFloatingItem == null)
            {
                MessageBox.Show("请先选中一个浮动图表窗口！");
                return;
            }

            // 2. 提取浮动项中的 CartesianChart（你的浮动项 Content 就是 Chart）
            var targetChart = activeFloatingItem.Content as CartesianChart;
            if (targetChart == null)
            {
                MessageBox.Show("选中的浮动窗口中未找到图表！");
                return;
            }

            CartesianChart chart = targetChart;


            //var axes = MyChart.XAxes;


            // 3. 修复：必须显式添加X/Y轴，且匹配int数据范围（核心中的核心）
            var xAxes = new Axis[]
            {
                new Axis
                {
                    Name = "X 轴",
                    Labeler = value => $"{(int)value}", // 标签显示整数索引
                }
            };

            var yAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Y 轴",
                    Labeler = value => $"{(int)value}", // 关键：标签显示整数，去掉小数位
                }
            };
            chart.XAxes = xAxes;
            chart.YAxes = yAxes;

           


            var series = new ObservableCollection<ISeries>();
            //var line = new LineSeries<ObservablePoint>{ Name = "温度",Values = GetDatas()};
            var line = new LineSeries<int> { Name = "温度", Values = GetIntDatas() };
            series.Add(line);
            //chart.SeriesSource = series;
            Binding binding = new Binding();
            binding.Source = series;
            BindingOperations.SetBinding(chart, CartesianChart.SeriesSourceProperty, binding);


        }




        private ObservableCollection<ObservablePoint> GetDatas()
        {
            ObservableCollection<ObservablePoint> datas = new ObservableCollection<ObservablePoint>();
            Random random = new Random();
            for (int i = 0; i < 300; i++)
            {
                datas.Add(new ObservablePoint() { X = i, Y = random.Next(0, 20) });
            }
            return datas;
        }

        private ObservableCollection<int> GetIntDatas()
        {
            ObservableCollection<int> datas = new ObservableCollection<int>();
            Random random = new Random();
            for (int i = 0; i < 300; i++)
            {
                datas.Add(random.Next(0, 20));
            }
            return datas;
        }
    }
}
