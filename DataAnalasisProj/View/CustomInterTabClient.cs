using Dragablz;
using System.Windows;

namespace CS.DataAnalasis.View
{
    public class CustomInterTabClient : IInterTabClient
    {
        // 创建浮动窗口时，显示系统标题栏（自带最大化/最小化/关闭）
        public INewTabHost<Window> GetNewHost(IInterTabClient interTabClient, object partition, TabablzControl source)
        {
            // 创建新的浮动窗口
            var floatWindow = new Window
            {
                Width = 400,
                Height = 300,
                WindowStyle = WindowStyle.SingleBorderWindow, // 显示系统标题栏（关键）
                ResizeMode = ResizeMode.CanResizeWithGrip,    // 允许调整窗口大小
                ShowInTaskbar = false
            };

            // 创建新的Tab控件
            var newTabControl = new TabablzControl
            {
                InterTabController = new InterTabController { InterTabClient = this }
            };

            floatWindow.Content = newTabControl;
            return new NewTabHost<Window>(floatWindow, newTabControl);
        }

        // 标签为空时关闭窗口
        public TabEmptiedResponse TabEmptiedHandler(TabablzControl tabControl, Window window)
        {
            return TabEmptiedResponse.CloseWindowOrLayoutBranch;
        }
    }
}
