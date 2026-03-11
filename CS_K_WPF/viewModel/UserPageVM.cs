using CommunityToolkit.Mvvm.ComponentModel;
using CS_K_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CS_K_WPF.viewModel
{
    public partial class UserPageVM: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Schoolclass> studentCollections = new ObservableCollection<Schoolclass>();

        [ObservableProperty]
        private ButtonCLass myButtonOne;

        [ObservableProperty]        
        private ObservableCollection<ButtonCLass> myButtonCollections = new ObservableCollection<ButtonCLass>();

        public UserPageVM()
        {
            studentCollections.Add(new Schoolclass() { Name = "张三", Age = 18, StudentId = 2023001 });
            studentCollections.Add(new Schoolclass() { Name = "李四", Age = 19, StudentId = 2023002 });
            studentCollections.Add(new Schoolclass() { Name = "王五", Age = 20, StudentId = 2023003 });

            //@"Resource/PhonePng.png" 
            MyButtonOne = new ButtonCLass() { Txt = "按钮1", ImgPath = new BitmapImage(new Uri(@"/Resource/PhonePng.png",UriKind.Relative)) };

            MyButtonCollections.Add(MyButtonOne);
            MyButtonCollections.Add(new ButtonCLass() { Txt = "ggg", ImgPath = new BitmapImage(new Uri(@"/Resource/AnimaPng.png", UriKind.Relative)) });
            MyButtonCollections.Add(new ButtonCLass() { Txt = "啥", ImgPath = new BitmapImage(new Uri(@"/Resource/LightPng.png", UriKind.Relative)) });


        }
    }
}
