using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CS_K_WPF.Model
{
    public class MyRoutedEventClass
    {
        public string Name { get; set; }

        public static readonly RoutedEvent NameChangedEvent =
      EventManager.RegisterRoutedEvent(
        "NameChanged",
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), //事件处理器的类型。该类型必须是委托类型，且不能为 null。
        typeof(MyRoutedEventClass)); //路由事件的宿主类型(是定义者的类型，不是触发者)，且不能为 null

        //public event RoutedEventHandler MyEvent
        //{
        //    add { AddNameChangedHandler(NameChangedEvent, value); }
        //    remove { RemoveNameChangedHandler(NameChangedEvent, value); }
        //}



        /// <summary>
        /// （AddNameChangedHandler是给XAML 识别用）
        /// </summary>
        public static void AddNameChangedHandler(DependencyObject myEventEvent, RoutedEventHandler value)
        {
            UIElement ule = myEventEvent as UIElement; //UIElement才有 RaiseEvent、AddHandler、RemoveHandler 方法
            if (ule != null)
            {
                ule.AddHandler(MyRoutedEventClass.NameChangedEvent, value);
            }
        }
        public static void RemoveNameChangedHandler(DependencyObject myEventEvent, RoutedEventHandler value)
        {
            UIElement ule = myEventEvent as UIElement;
            if (ule != null)
            {
                ule.RemoveHandler(MyRoutedEventClass.NameChangedEvent, value);
            }
        }


    }
}
