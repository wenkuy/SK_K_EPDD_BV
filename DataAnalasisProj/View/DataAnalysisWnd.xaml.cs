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
using LiveChartsGeneratedCode;
using System.Windows.Input;
using LiveChartsCore.Measure;

namespace DataAnalasisProj.View
{
    /// <summary>
    /// DataAnalysisWnd.xaml 的交互逻辑
    /// </summary>
    public partial class DataAnalysisWnd : Window
    {
        public ObservableCollection<SimpleViewModel> ToolItems { get; }
        private Dictionary<SourceGenCartesianChart, ObservableCollection<ISeries>> chartAndSeries = new Dictionary<SourceGenCartesianChart, ObservableCollection<ISeries>>();
        private Dictionary<SourceGenCartesianChart, ObservableCollection<TabItem>> tabItems = new Dictionary<SourceGenCartesianChart, ObservableCollection<TabItem>>();

        private string floatingPanelHeaderName;
        private CartesianChart currentChart;

        public DataAnalysisWnd()
        {
            InitializeComponent();
            ToolItems = new ObservableCollection<SimpleViewModel>();//界面xaml上绑定了这个类的两个属性
            DragLyout.FloatingItemsSource = ToolItems; // 设置浮动项的数据源

        }

        /// <summary>
        /// 为浮动项添加内容：添加Chart
        /// </summary>
        private void AddChartButton_Click(object sender, RoutedEventArgs e)
        {
            CartesianChart chart = new CartesianChart()
            {
                Background = Brushes.LightBlue, // 背景色
                ZoomMode = ZoomAndPanMode.X, // 缩放模式：仅X轴缩放/平移
                LegendTextSize = 15,
                LegendPosition = LegendPosition.Top
            };

            chart.MouseLeftButtonDown += CheckedChart;

            ToolItems.Add(new SimpleViewModel()
            {
                Name = $"图表 {ToolItems.Count + 1}",
                SimpleContent = chart,
            });

            chartAndSeries.Add(chart, new ObservableCollection<ISeries>());
        }


        private void CheckedChart(object obj, MouseButtonEventArgs e)
        {
            //获取当前chart的parent的header内容
            var chart = obj as CartesianChart;
            if (null == chart) return;
            var parentItem = chart.Parent as HeaderedDragablzItem;
            floatingPanelHeaderName = parentItem?.HeaderContent?.ToString();
            currentChart = chart;

            //显示该chart的items
            TabCtrl.Items.Clear();
            if (!tabItems.ContainsKey(currentChart))
            {
                tabItems.Add(currentChart, new ObservableCollection<TabItem>());
            }

            if (!tabItems.TryGetValue(currentChart, out var collection) || collection?.Count == 0)
            {
                return;
            }
            foreach (var item in tabItems[currentChart])
            {
                TabCtrl.Items.Add(item);
                item.IsSelected = true;
            }
        }

        private void AddSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            TabItem item = new TabItem();
            item.Header = $"曲线 {TabCtrl.Items.Count + 1}";

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

            //TabCtrl.Items.Add(item);
            TabCtrl.Items.Clear();
            tabItems[currentChart].Add(item);
            foreach (var itm in tabItems[currentChart])
            {
                TabCtrl.Items.Add(itm);
                //选中该item，内容才会显示
                itm.IsSelected = true;
            }
        }


        /// <summary>
        /// 删除曲线
        /// </summary>
        private void DeleteSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            int index = tabControl_selectedindex;
            if (0 == TabCtrl.Items.Count) return;
            //TabCtrl.Items.RemoveAt(TabCtrl.Items.Count - 1);

            tabItems[currentChart].RemoveAt(index);
            TabCtrl.Items.Clear();
            foreach (var itm in tabItems[currentChart])
            {
                TabCtrl.Items.Add(itm);
                itm.IsSelected = true;
            }

            chartAndSeries[currentChart].RemoveAt(index);
            currentChart.Series = chartAndSeries[currentChart];
        }




        /// <summary>
        /// 确认
        /// </summary>
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (null == floatingPanelHeaderName) return;
            SimpleViewModel activeFloatingItem = null;
            foreach (var item in DragLyout.FloatingItems)
            {
                if (floatingPanelHeaderName == ((SimpleViewModel)item).Name)
                {
                    activeFloatingItem = (SimpleViewModel)item;
                    break;
                }
            }

            if (activeFloatingItem == null)
            {
                MessageBox.Show("请先选中一个浮动图表窗口！");
                return;
            }

            // 2. 提取浮动项中的 CartesianChart（你的浮动项 Content 就是 Chart）
            var targetChart = activeFloatingItem.SimpleContent as CartesianChart;
            if (targetChart == null)
            {
                MessageBox.Show("选中的浮动窗口中未找到图表！");
                return;
            }

            CartesianChart chart = targetChart;


            #region 轴设置(需要可设)

            // 3. 修复：必须显式添加X/Y轴，且匹配int数据范围（核心中的核心）
            //var xAxes = new Axis[]
            //{
            //    new Axis
            //    {
            //        Name = "X 轴",
            //        Labeler = value => $"{(int)value}", // 标签显示整数索引
            //        MinLimit = 0,
            //        MaxLimit = 300,
            //         // 强制显示刻度，确保轴渲染完整
            //       ShowSeparatorLines = true,
            //    }
            //};

            //var yAxes = new Axis[]
            //{
            //    new Axis
            //    {
            //        Name = "Y 轴",
            //        Labeler = value => $"{(int)value}", // 关键：标签显示整数，去掉小数位
            //        MinLimit =0,
            //        MaxLimit = 30,
            //        ShowSeparatorLines = true,
            //    }
            //};
            //chart.XAxes = xAxes;
            //chart.YAxes = yAxes;

            #endregion


            var series = new ObservableCollection<ISeries>();
            //var line = new LineSeries<ObservablePoint>{ Name = "温度",Values = GetDatas()};
            var line = new LineSeries<int>
            {
                Name = "温度",
                Values = GetIntDatas(),
                //Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 3 }, //不写就是随即
                LineSmoothness = 0.65, // 折线平滑度（0=直线，1=最弯曲，默认0.65）
                EnableNullSplitting = true, // 遇到null值时断开折线（默认true）
                //Fill = new SolidColorPaint(SKColors.Blue.WithAlpha(60)), //默认Fill


                // 数据点样式（几何图形）
                GeometrySize = 1, // 数据点几何图形大小（默认14）
                GeometryFill = new SolidColorPaint(SKColors.White), // 数据点填充色
                GeometryStroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 }, // 数据点边框
            };
            chartAndSeries[chart].Add(line);
            chart.Series = chartAndSeries[chart];
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

        private readonly DragablzItemsControl _floatingItems;


        private int tabControl_selectedindex;


        private void TabCtrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tabControl_selectedindex = TabCtrl.SelectedIndex;
        }
    }
}
