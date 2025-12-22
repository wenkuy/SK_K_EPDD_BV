using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.viewModel
{
    public class NavigationPageVM
    {
        public RelayCommand HomePageCommand { get; }
        public RelayCommand MonitorPageCommand { get; }
        public RelayCommand EquipmentPageCommand { get; }
        public RelayCommand UserPageCommand { get; }
        public RelayCommand SettingPageCommand { get; }

        public NavigationPageVM()
        {
            HomePageCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new Uri("HomePage.xaml", UriKind.Relative)));
            MonitorPageCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new Uri("MonitorPage.xaml", UriKind.Relative)));
            EquipmentPageCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new Uri("EquipmentPage.xaml", UriKind.Relative)));
            UserPageCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new Uri("UserPage.xaml", UriKind.Relative)));
            SettingPageCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new Uri("SettingPage.xaml", UriKind.Relative)));


            //首次主动显示HomePage页面
            WeakReferenceMessenger.Default.Send(new Uri("HomePage.xaml", UriKind.Relative));
        }

       
    }
}
