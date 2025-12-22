using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.viewModel
{
    public partial class MainPageVM : ObservableObject
    {
        [ObservableProperty]
        private Uri pagePath;

        public MainPageVM()
        {
            WeakReferenceMessenger.Default.Register<Uri>(this, GetPagePath);
        }

        private void GetPagePath(object recipient, Uri message)
        {
            PagePath = message;
        }

       
    }
}
