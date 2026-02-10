using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CS.Base
{
    public class CommonDispatcherHelper
    {
       //private static  SynchronizationContext uiDispatcher => SynchronizationContext.Current; //因为是获取当前线程的上下文，所以这里实例就得掐准时机！！！
       private static  SynchronizationContext uiDispatcher ;


        /// <summary>
        /// 去一个能确定是ui线程在执行的地方调用此方法，从而确保获取UI线程上下文
        /// </summary>
        public static void Init()
        {
            uiDispatcher = SynchronizationContext.Current;
        }

        public static void ExecuteOnUiThread(Action action)
        {
            
            if (action == null || null == uiDispatcher) return;

            // 如果当前已经在UI线程，直接执行
            if (SynchronizationContext.Current == uiDispatcher)
            {
                action.Invoke();
            }
            else
            {
                // 调度到UI线程执行（同步）相当于WPF版本的Dispatcher.Invoke()（同步）
                uiDispatcher.Send(_ => action.Invoke(), null);
            }
        }

        public static void BeginExecuteOnUiThread(Action action)
        {
            if (action == null || null == uiDispatcher) return;

            if (SynchronizationContext.Current == uiDispatcher)
            {
                action.Invoke();
            }
            else
            {
                // 调度到UI线程执行（异步）相当于WPF版本的Dispatcher.BeginInvoke()（异步）
                uiDispatcher.Post(_ => action.Invoke(), null);
            }
        }
    }
}
