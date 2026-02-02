using Dragablz;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;
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
        public ObservableCollection<SimpleViewModel> ToolItems { get; }
        public DataAnalysisWnd()
        {
            InitializeComponent();
            ToolItems = new ObservableCollection<SimpleViewModel>();
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
            ToolItems.Add(new SimpleViewModel()
            {
                Name = $"图表 {ToolItems.Count + 1}",
                SimpleContent = new CartesianChart()
                {
                    // 关键设置：让图表自动拉伸填满父容器
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Background = Brushes.LightBlue, // 加背景色，能直观看到图表控件
              
                },
            });

            //MyGrid.Children.Add(new CartesianChart()
            //{
            //    // 关键设置：让图表自动拉伸填满父容器
            //    HorizontalAlignment = HorizontalAlignment.Stretch,
            //    VerticalAlignment = VerticalAlignment.Stretch,
            //    Background = Brushes.LightBlue, // 加背景色，能直观看到图表控件

            //});
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
            var kk = DragLyout.FloatingItems[0];
            var activeFloatingItem = kk as SimpleViewModel;
            if (activeFloatingItem == null)
            {
                MessageBox.Show("请先选中一个浮动图表窗口！");
                return;
            }

            // 2. 提取浮动项中的 CartesianChart（你的浮动项 Content 就是 Chart）
            //var targetChart = MyGrid.Children[0] as CartesianChart;
            var targetChart = activeFloatingItem.SimpleContent as CartesianChart;
            if (targetChart == null)
            {
                MessageBox.Show("选中的浮动窗口中未找到图表！");
                return;
            }

            CartesianChart chart = targetChart;




            // 3. 修复：必须显式添加X/Y轴，且匹配int数据范围（核心中的核心）
            var xAxes = new Axis[]
            {
                new Axis
                {
                    Name = "X 轴",
                    Labeler = value => $"{(int)value}", // 标签显示整数索引
                    MinLimit = 0,
                    MaxLimit = 300,
                     // 强制显示刻度，确保轴渲染完整
                   ShowSeparatorLines = true,
                }
            };

            var yAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Y 轴",
                    Labeler = value => $"{(int)value}", // 关键：标签显示整数，去掉小数位
                    MinLimit =0,
                    MaxLimit = 30,
                    ShowSeparatorLines = true,
                }
            };
            chart.XAxes = xAxes;
            chart.YAxes = yAxes;




            var series = new ObservableCollection<ISeries>();
            //var line = new LineSeries<ObservablePoint>{ Name = "温度",Values = GetDatas()};
            var line = new LineSeries<int>
            {
                Name = "温度",
                Values = GetIntDatas(),
                Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 3 },
                LineSmoothness = 0.65, // 折线平滑度（0=直线，1=最弯曲，默认0.65）
                EnableNullSplitting = true, // 遇到null值时断开折线（默认true）
                Fill = new SolidColorPaint(SKColors.Blue.WithAlpha(60)),


                // 数据点样式（几何图形）
                GeometrySize = 14, // 数据点几何图形大小（默认14）
                GeometryFill = new SolidColorPaint(SKColors.White), // 数据点填充色
                GeometryStroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 }, // 数据点边框
            };
            series.Add(line);
            chart.Series = series;

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
