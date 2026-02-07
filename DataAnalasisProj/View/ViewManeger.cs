using CommunityToolkit.Mvvm.Messaging;
using CSK.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CS.DataAnalasis.View
{
    public class ViewManeger
    {
        /// <summary>
        /// 窗体注册管理器 --- 这个是定死的
        /// </summary>
        public static void ViewRegisterManeger()
        {
            //typeOf() 返回该实例，但仅一个占位象征，徒有表象(仅存在元数据，不存在业务数据)，因此不能访问其成员，
            WeakReferenceMessenger.Default.Register<OpenAnalysisWindowMes>(typeof(DataAnalysisWnd), (obj, message) => { (new DataAnalysisWnd()).Show(); });

            //内存泄漏风险，因为传了一个“幽灵实例”new DataAnalysisWnd()
            //WeakReferenceMessenger.Default.Register<OpenAnalysisWindowMes>(new DataAnalysisWnd(), (obj, message) => { (new DataAnalysisWnd()).Show(); });
        }

        /// <summary>
        /// 窗体注册管理器 --- 泛型灵活方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void ViewRegisterManegerGeneral<TMes, TWnd>() where TWnd : Window, new() where TMes : class
        {
            WeakReferenceMessenger.Default.Register<TMes>(typeof(TWnd), (obj, message) => { (new TWnd()).Show(); });
        }
    }
}
