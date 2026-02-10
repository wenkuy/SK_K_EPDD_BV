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

        public HomePageVM LocHomePageVM { get; init; } //不要{set}，杜绝被修改
        public NavigationPageVM LocNavigationPageVM { get; init; }
        public MainPageVM LocMainPageVM { get; init; }


        public ModelLocator(HomePageVM homePageVM,NavigationPageVM navigationPageVM, MainPageVM mainPageVM)
        {
            LocHomePageVM = homePageVM; //【2】享受服务
            LocNavigationPageVM = navigationPageVM;
            LocMainPageVM = mainPageVM;
        }
    }
}
