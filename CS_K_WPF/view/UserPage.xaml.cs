using CS_K_WPF.viewModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CS_K_WPF.view
{
    /// <summary>
    /// UserPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
           
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            ((UserPageVM)this.DataContext).Exit();
            this.DataContext = null; // 解除数据绑定，帮助垃圾回收器回收内存
        }
    }
}
