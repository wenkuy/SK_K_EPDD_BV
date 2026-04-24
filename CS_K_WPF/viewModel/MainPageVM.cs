using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CSK.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CS_K_WPF.viewModel
{
    public partial class MainPageVM : ObservableObject
    {
        [ObservableProperty]
        private Uri pagePath;

       

        public MainPageVM(IMessageBus messageBus)
        {
            //WeakReferenceMessenger.Default.Register<Uri>(this, GetPagePath);
            messageBus.Register<Uri>(GetPagePath);
        }

        private void GetPagePath(object recipient, Uri message)
        {
            //if(message.ToString().Contains("UserPage"))
            //{
            //    MessageBox.Show("UserPage听过异步加载方式实现！");
            //    return;
            //}
            PagePath = message;
        }

       
    }
}
