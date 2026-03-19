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

namespace CS_K_WPF.view
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private Point lastPoint;

        private void MainViewport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) //按下鼠标左键
            {
                Mouse.Capture(mainViewport); //捕获鼠标输入到MainViewport元素

                lastPoint = e.GetPosition(mainViewport); //获取当前鼠标位置
            }
        }

        private void MainViewport_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                //水平方向的偏移给Y轴角度，才是沿着Y轴转动；
                axisY.Angle = axisY.Angle + (e.GetPosition(mainViewport).X - lastPoint.X); 
                //垂直方向的偏移给X轴角度，才是沿着X轴旋转
                axisX.Angle = axisX.Angle + (e.GetPosition(mainViewport).Y - lastPoint.Y);
                lastPoint = e.GetPosition(mainViewport);
            }
        }

        private void MainViewport_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.Capture(null); //释放鼠标捕获
        }

        private void MainViewport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Mouse.Capture(null); //释放鼠标捕获。 这个写这里不行，因为自己的弹窗一直弹跳，除非干掉那个。
            if(e.RightButton == MouseButtonState.Pressed)
            {
                Mouse.Capture(null); //释放鼠标捕获
            }
        }
    }
}
