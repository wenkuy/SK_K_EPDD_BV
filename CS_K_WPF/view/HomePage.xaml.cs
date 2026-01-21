using CommunityToolkit.Mvvm.Messaging;
using CS_K_WPF.view;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 方式1- 大型项目
            //WeakReferenceMessenger.Default.Register<DataAnalysisWnd>(this, () => { });

            // 方式2 - 直接DI获取实例对象（与直接[new DataAnalysisWnd()] 就差一个DI）
            //App.GlobalServiceProvider.GetRequiredService<DataAnalysisWnd>().Show();

            // 方式3 - DI和窗口操作封装结合
            IWindowOperation wnd = App.GlobalServiceProvider.GetRequiredService<IWindowOperation>();
            wnd.OpenWindow<DataAnalysisWnd>();
        }
    }
}
