using CS_K_WPF.ReceiveMess;
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

            //???? 临时放一下，后续解决如何在合适的地方让其实例化，实现委托绑定，要不然udp的Action没人委托，没法传出来 
            ReceiveMessages receiveMessages = new ReceiveMessages();  

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