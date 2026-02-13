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
        public HomePageVM LocHomePageVM { get => serviceProvider.GetRequiredService<HomePageVM>(); }
        public NavigationPageVM LocNavigationPageVM { get => serviceProvider.GetRequiredService<NavigationPageVM>(); }
        public MainPageVM LocMainPageVM { get => serviceProvider.GetRequiredService<MainPageVM>(); }

        public ExceptionWndVM LocExceptionWndVM { get => serviceProvider.GetRequiredService<ExceptionWndVM>(); }




        private IServiceProvider serviceProvider { get; init; }//不要{set}，杜绝被修改
        public ModelLocator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }


    }
}
