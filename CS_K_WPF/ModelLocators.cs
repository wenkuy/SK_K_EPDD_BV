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


        public ModelLocator()
        {
            Register();
        }

        private void Register()
        {
            using (var root = new ServiceCollection()
                 .AddScoped<HomePageVM>()
                 .BuildServiceProvider())
            {
                using (var scop = root.CreateScope())
                {
                    LocHomePageVM = scop.ServiceProvider.GetRequiredService<HomePageVM>();
                }
            }
        }

    }
}
