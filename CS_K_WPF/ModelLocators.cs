using CS_K_WPF.viewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using var root = new ServiceCollection()
                 .AddSingleton<HomePageVM>()  //【1】先注册服务
                 .AddSingleton<NavigationPageVM>()
                 .AddSingleton<MainPageVM>()
                 .BuildServiceProvider();

            LocHomePageVM = root.GetRequiredService<HomePageVM>(); //【2】享受服务
            LocNavigationPageVM = root.GetRequiredService<NavigationPageVM>();
            LocMainPageVM = root.GetRequiredService<MainPageVM>();
        }

        #region 已注释代码
        ///// <summary>
        ///// DI注入
        ///// </summary>
        //private void Register()
        //{
        //    using (var root = new ServiceCollection()
        //         .AddScoped<HomePageVM>()  //【1】先注册服务
        //         .AddScoped<NavigationPageVM>()
        //         .AddScoped<MainPageVM>()
        //         .BuildServiceProvider())
        //    {
        //        using (var scop = root.CreateScope())
        //        {
        //            LocHomePageVM = scop.ServiceProvider.GetRequiredService<HomePageVM>(); //【2】享受服务
        //            LocNavigationPageVM = scop.ServiceProvider.GetRequiredService<NavigationPageVM>();
        //            LocMainPageVM = scop.ServiceProvider.GetRequiredService<MainPageVM>();
        //        }
        //    }
        //}
        #endregion
    }
}
