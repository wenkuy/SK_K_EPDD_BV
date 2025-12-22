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

        public static ModelLocator Instance => new();

        public HomePageVM LocHomePageVM { get; set; }
        public NavigationPageVM LocNavigationPageVM { get; set; }
        public MainPageVM LocMainPageVM { get; set; }


        public ModelLocator()
        {
            Register();
        }

        /// <summary>
        /// DI注入
        /// </summary>
        private void Register()
        {
            using (var root = new ServiceCollection()
                 .AddScoped<HomePageVM>()  //【1】先注册服务
                 .AddScoped<NavigationPageVM>()
                 .AddScoped<MainPageVM>()
                 .BuildServiceProvider())
            {
                using (var scop = root.CreateScope())
                {
                    LocHomePageVM = scop.ServiceProvider.GetRequiredService<HomePageVM>(); //【2】享受服务
                    LocNavigationPageVM = scop.ServiceProvider.GetRequiredService<NavigationPageVM>();
                    LocMainPageVM = scop.ServiceProvider.GetRequiredService<MainPageVM>();
                }
            }
        }

    }
}
