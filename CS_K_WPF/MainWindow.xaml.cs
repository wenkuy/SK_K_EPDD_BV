using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TCPServer;
using UdpSenderProj;

namespace CS_K_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Task.Run(() =>
            {

                TcpServerFun tcpServer = new TcpServerFun();
                tcpServer.TcpServerRun();
            });

            Task.Run(() =>
            {
                UdpReceive udpReceive = new UdpReceive();
                udpReceive.UdpReceiveRun();
            });
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(WindowState.Maximized == this.WindowState)
            {
                this.WindowState = WindowState.Normal;
            }

            // 调用Window的DragMove()方法，实现拖动
            this.DragMove();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
    }
}