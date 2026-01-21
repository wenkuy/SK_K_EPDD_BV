using CS_K_WPF.viewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_K_WPF
{
    public class ModelLocator
    {
        public static ModelLocator Instance { get; } //单加get防止外部修改,被置null
        static ModelLocator()
        {
            Instance = new ModelLocator();
        }

        public HomePageVM LocHomePageVM { get; } //不要{set}，杜绝被修改
        public NavigationPageVM LocNavigationPageVM { get; }
        public MainPageVM LocMainPageVM { get; }

        // 构造私有化，外部再无创建实例可能性
        private ModelLocator()
        {
            //using释放容器  DI注入   
            var root = App.GlobalServiceProvider;
            //.AddSingleton<HomePageVM>()  //【1】先注册服务
            //.AddSingleton<NavigationPageVM>()
            //.AddSingleton<MainPageVM>()
            //.BuildServiceProvider();

            LocHomePageVM = root.GetRequiredService<HomePageVM>(); //【2】享受服务
            LocNavigationPageVM = root.GetRequiredService<NavigationPageVM>();
            LocMainPageVM = root.GetRequiredService<MainPageVM>();
        }
    }
}
