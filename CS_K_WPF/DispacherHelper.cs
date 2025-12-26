using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CS_K_WPF
{
    public class DispacherHelper
    {
        //必须static 一直存在
        private static Dispatcher dispatcher => Application.Current.Dispatcher;

        /// <summary>
        /// 同步(阻塞，等UI结果)
        /// </summary>
        /// <param name="action"></param>
        public static void ExecuteOnUiThread(Action action)
        {
            if (action == null || dispatcher == null) return;

            // 如果当前已经是UI线程，直接执行，避免不必要的调度
            if (dispatcher.CheckAccess())
            {
                action.Invoke();
            }
            else
            {
                // 同步调度到UI线程，带超时保护，避免卡死
                //dispatcher.Invoke(action, DispatcherPriority.Normal, TimeSpan.FromSeconds(5));
                dispatcher.Invoke(action);
            }
        }

        /// <summary>
        /// 异步（非阻塞，不等UI结果）
        /// </summary>
        /// <param name="action"></param>
        public static void BeginExecuteOnUiThread(Action action)
        {
            if (action == null) return;

            if (dispatcher.CheckAccess())
            {
                action.Invoke();
            }
            else
            {
                dispatcher.BeginInvoke(action, DispatcherPriority.Normal);
            }

        }
    }
}
