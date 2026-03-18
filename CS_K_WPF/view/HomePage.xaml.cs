using CommunityToolkit.Mvvm.Messaging;
using CS_K_WPF.view;
using CSK.Core;
using CS.DataAnalasis.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CS_K_WPF
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            this.Unloaded += HomePage_Unloaded;
        }

        private void HomePage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= HomePage_Unloaded;
            WeakReferenceMessenger.Default.UnregisterAll(this);
            this.DataContext = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //【内部方式】（放弃）
            // 方式1- 大型项目
            //WeakReferenceMessenger.Default....;

            // 方式2 - 直接DI获取实例对象（与直接[new DataAnalysisWnd()] 就差一个DI）
            //App.GlobalServiceProvider.GetRequiredService<DataAnalysisWnd>().Show();

            // 方式3 - DI和窗口操作封装结合
            //IWindowOperation wnd = App.GlobalServiceProvider.GetRequiredService<IWindowOperation>();
            //wnd.OpenWindow<DataAnalysisWnd>();


            //【外部模块方式】（使用）
            // 方式1 - 调用测试：引用，调出弹窗，ok[这个经测试，耦合高，所以放弃该方式]
            //new DataAnalasisProj.View.DataAnalysisWnd().Show();

            //方式1 - 主项目通过CommunicationToolKit.MVVM的方式实现对DataAnalysisProj项目的窗体的调用
            WeakReferenceMessenger.Default.Send(new OpenAnalysisWindowMes());
        }
    }
}
