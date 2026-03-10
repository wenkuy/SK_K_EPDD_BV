using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CS_K_WPF
{
    public class MyCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;//事件是由开发者触发，wpf框架订阅
        private Action<object> ExecuteAction { get; set; }
        private Action ExecuteActionNoParam { get; set; } //为什么不用Func是因为Execute方法没有返回值，所以用Action更合适
        private object? _parameter; //这个参数是用来传递给Execute方法的参数，可以在构造函数中设置，也可以通过属性设置
        private Func<bool>? _canExecuteFunc; //这个是用来判断命令是否可以执行的函数，可以在构造函数中设置，也可以通过属性设置

        public MyCommand(Action<object> act, object parameter, Func<bool> func = null)
        {
            if (null != act) ExecuteAction = act; else throw new ArgumentNullException(nameof(act), "function of MyCommand:  act cannot be null."); //如果没有传入执行逻辑，抛出异常
            if (null != parameter) _parameter = parameter; else throw new ArgumentNullException(nameof(act), "function of MyCommand:  parameter cannot be null.");
            if (null != func) _canExecuteFunc = func;
        }

        public MyCommand(Action act, Func<bool> func = null)
        {
            if (null != act) ExecuteActionNoParam = act; else throw new ArgumentNullException(nameof(act), "function of MyCommand:  act cannot be null."); 
        }

        /// <summary>
        /// 当外部触发CanExecuteChanged时，系统会调用CanExecute方法来判断命令是否可以执行，如果返回true，UI元素就会启用，否则就会禁用。
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            //如果传了逻辑就判断，否则默认可以执行true
            return null != _canExecuteFunc ? _canExecuteFunc.Invoke() : true;
            
        }

     

        /// <summary>
        /// 当判断条件变了，调用该方法通知框架，框架会重新调用CanExecute方法来判断命令是否可以执行，从而更新UI元素的状态。
        /// </summary>
        public void NotifyCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);    //this 表示触发事件的对象
           
        }

        /// <summary>
        /// UI元素被点击时调用该方法
        /// </summary>
        public void Execute(object? parameter)
        {
            if (null == _parameter)
            {
                ExecuteActionNoParam?.Invoke();
            }
            else
            {
                ExecuteAction?.Invoke(_parameter);
            }
        }

        public void DleteCanExecuteChanged()
        {
            CanExecuteChanged = null; //取消所有订阅者的订阅，释放资源
        }
    }
}
