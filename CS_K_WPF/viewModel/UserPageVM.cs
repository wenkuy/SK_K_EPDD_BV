using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CS_K_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CS_K_WPF.viewModel
{
    public partial class UserPageVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Schoolclass> studentCollections = new ObservableCollection<Schoolclass>();

        [ObservableProperty]
        private ButtonCLass myButtonOne;

        [ObservableProperty]
        private ObservableCollection<ButtonCLass> myButtonCollections = new ObservableCollection<ButtonCLass>();

        [ObservableProperty]
        private ObservableCollection<StudentIDclass> studentIDCollects = new ObservableCollection<StudentIDclass>();



        [ObservableProperty]
        private double eletricity = 0f;

        [ObservableProperty]
        private float numberTest;

        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token;

        public UserPageVM()
        {
            token = cts.Token;

            RelayCommand<object> cmd = new RelayCommand<object>(param =>
            {
                MessageBox.Show($"现在的index是：{param.ToString()}");

            });

            StudentIDCollects.Add(new StudentIDclass() { Name = "学号1", Id = 2023001 });
            StudentIDCollects.Add(new StudentIDclass() { Name = "学号2", Id = 2023002 });
            StudentIDCollects.Add(new StudentIDclass() { Name = "学号3", Id = 2023003 });
            StudentIDCollects.Add(new StudentIDclass() { Name = "学号4", Id = 2023004 });
            StudentIDCollects.Add(new StudentIDclass() { Name = "学号5", Id = 2023005 });

            studentCollections.Add(new Schoolclass() { Name = "张三", Age = 18, StudentId = 2023001, CMD = cmd });
            studentCollections.Add(new Schoolclass() { Name = "李四", Age = 19, StudentId = 2023002, CMD = cmd });
            studentCollections.Add(new Schoolclass() { Name = "王五", Age = 20, StudentId = 2023003, CMD = cmd });
            studentCollections.Add(new Schoolclass() { Name = "laoliu", Age = 30, StudentId = 2023004, CMD = cmd });
            studentCollections.Add(new Schoolclass() { Name = "c城市", Age = 26, StudentId = 2023005, CMD = cmd });

            //@"Resource/PhonePng.png" 
            MyButtonOne = new ButtonCLass() { Txt = "按钮1", ImgPath = new BitmapImage(new Uri(@"/Resource/PhonePng.png", UriKind.Relative)) };

            List<ButtonCLass> Butons = new List<ButtonCLass>();
            AddElment(Butons);
            MyButtonCollections = new ObservableCollection<ButtonCLass>(Butons);
            MyButtonCollections.Add(MyButtonOne);
            MyButtonCollections.Add(new ButtonCLass() { Txt = "ggg", ImgPath = new BitmapImage(new Uri(@"/Resource/AnimaPng.png", UriKind.Relative)) });
            MyButtonCollections.Add(new ButtonCLass() { Txt = "啥", ImgPath = new BitmapImage(new Uri(@"/Resource/LightPng.png", UriKind.Relative)) });
            MyButtonCollections.Add(new ButtonCLass() { Txt = "啥1", ImgPath = new BitmapImage(new Uri(@"/Resource/LightPng.png", UriKind.Relative)) });

            



            Random random = new Random();
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {

                    Eletricity = random.Next(20, 100) * 1d; // 生成0到100之间的随机数
                    await Task.Delay(1000); // 每隔1秒更新一次

                }

            },token);

        }

        public void AddElment(List<ButtonCLass> Butons)
        {


            for (int i = 2; i < 52000; i++)
                Butons.Add(new ButtonCLass() { Txt = $"啥--{i}", ImgPath = new BitmapImage(new Uri(@"/Resource/LightPng.png", UriKind.Relative)) });


        }

        public void Exit()
        {
            //token.ThrowIfCancellationRequested();
             cts.Cancel(); // 取消任务
        }
    }
}
