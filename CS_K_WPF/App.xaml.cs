using BaseProj;
using CommunityToolkit.Mvvm.Messaging;
using CS_K_WPF.viewModel;
using Database;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CS_K_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            ServiceProvider.Initialize();
        }

    }



}
