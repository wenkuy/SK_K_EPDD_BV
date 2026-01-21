using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CS_K_WPF
{
    public interface IWindowOperation
    {
        // 打开指定类型的窗口（支持传参数，比如给窗口VM传数据）
        public T OpenWindow<T>(object? parameter = null) where T : Window;

        // 关闭指定类型的窗口
        public void CloseWindow<T>() where T : Window;

        // 可选：获取窗口实例（方便后续操作，比如刷新数据）
        T? GetWindow<T>() where T : Window;
    }
}
