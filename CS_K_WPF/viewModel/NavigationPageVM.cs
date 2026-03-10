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
        //public RelayCommand HomePageCommand { get; }
        public MyCommand HomePageCommand { get; }
        public RelayCommand MonitorPageCommand { get; }
        public RelayCommand EquipmentPageCommand { get; }
        public RelayCommand UserPageCommand { get; }
        public RelayCommand SettingPageCommand { get; }

        public NavigationPageVM()
        {
            //这边是通过Message机制，发送页面路径的Uri消息
            //HomePageCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new Uri("HomePage.xaml", UriKind.Relative)));
            HomePageCommand = new MyCommand(() => 
                {
                    WeakReferenceMessenger.Default.Send(new Uri("HomePage.xaml", UriKind.Relative));
                   
                }
            
                );


            MonitorPageCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new Uri("MonitorPage.xaml", UriKind.Relative)));
            EquipmentPageCommand = new RelayCommand(() => WeakReferenceMessenger.Default.Send(new Uri("EquipmentPage.xaml", UriKind.Relative)));
            UserPageCommand = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.Send(new Uri("UserPage.xaml", UriKind.Relative));
                //HomePageCommand.DleteCanExecuteChanged();
                }
            
            );
            SettingPageCommand = new RelayCommand(() =>
               {
                   WeakReferenceMessenger.Default.Send(new Uri("SettingPage.xaml", UriKind.Relative));
                   //HomePageCommand.NotifyCanExecuteChanged();
                }
            );


            //首次主动显示HomePage页面【为什么这里没反应，首次没有自动加载首页】
            WeakReferenceMessenger.Default.Send(new Uri("HomePage.xaml", UriKind.Relative));
        }

       
    }
}
