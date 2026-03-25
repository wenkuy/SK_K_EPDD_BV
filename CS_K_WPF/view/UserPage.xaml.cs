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
            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5)); // 从0到1，持续0.5秒的动画
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5)); // 设置动画持续时间
            doubleAnimation.BeginTime = TimeSpan.FromSeconds(0); // 设置动画开始时间
            new Button().BeginAnimation(OpacityProperty, doubleAnimation); // 对Button1的Opacity属性应用动画
        }
    }
}
