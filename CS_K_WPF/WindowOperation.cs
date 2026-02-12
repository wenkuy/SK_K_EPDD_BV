using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CS_K_WPF
{
    public class WindowOperation : IWindowOperation
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, Window> _viewCache = new();

        /*你在 App 里构建的 GlobalServiceProvider 就是整个程序的 DI 容器本身。
         * 当你通过 GlobalServiceProvider.GetRequiredService<IWindowOperation>() 获取 WindowOperation 实例时，
         * DI 容器会自动把  它自己（IServiceProvider 接口的实现）注入到 WindowOperation 的构造函数里
         这样 WindowOperation 内部的 _serviceProvider 就是全局容器的引用。

        但是这是一种伪DI用法(也叫服务定位器用法ServiceLocator)，仅在特殊情况下，其他的服务获取还是的遵循正常的依赖注入方式，
        不能在构造函数里直接获取全局容器来获取服务实例，这样会导致代码耦合度过高，难以维护和测试。
        
         */
        public WindowOperation(IServiceProvider serviceProvider)  //DI会自己注入自己，这里获取全局的容器
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <exception cref="NotImplementedException"></exception>
        public T OpenWindow<T>(object? parameter = null) where T : Window
        {
            
            if(_viewCache.TryGetValue(typeof(T),out Window wxd) )
            {
                //存在则返回
                wxd.Show();
                return (T)wxd;
            }
            else
            {
                //不存在则缓存
                var wnd = _serviceProvider.GetRequiredService<T>();
                _viewCache.Add(typeof(T), wnd);
                wnd.Show();
                return wnd;
            }
         
        }


        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="NotImplementedException"></exception>
        public void CloseWindow<T>() where T : Window
        {
            _serviceProvider.GetRequiredService<T>().Close();
            _viewCache.Remove(typeof(T));
        }

        /// <summary>
        /// 获取窗口实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T? GetWindow<T>() where T : Window
        {
            return _serviceProvider.GetRequiredService<T>(); 
        }
    }
}
