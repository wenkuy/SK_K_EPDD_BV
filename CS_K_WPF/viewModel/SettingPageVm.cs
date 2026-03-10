using CommunityToolkit.Mvvm.ComponentModel;
using CS_K_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CS_K_WPF.viewModel
{
    public partial class SettingPageVm : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<TestClass> testClassCollections = new ObservableCollection<TestClass>();

        [ObservableProperty]
        private ObservableCollection<TreeViewLight> treeViewLightCollections = new ObservableCollection<TreeViewLight>();

        [ObservableProperty]
        private string myText;

        public SettingPageVm()
        {
            Tree1_Datas();
            Tree2_Datas();
        }

        private void Tree1_Datas()
        {
            MyText = "ffw";

            TestClass fo = new TestClass() { Id = 1, Name = "父1" };
            TestClass fo2 = new TestClass() { Id = 1, Name = "父2" };

            TestClass Chi = new TestClass() { Id = 10, Name = "子1" };
            TestClass Chi2 = new TestClass() { Id = 11, Name = "子2" };
            TestClass Chi3 = new TestClass() { Id = 21, Name = "子3" };

            TestClass ChiChi = new TestClass() { Id = 101, Name = "孙2" };
            Chi2.Children = new List<TestClass>() { ChiChi };


            fo.Children = new List<TestClass>() { Chi, Chi2 };
            fo2.Children = new List<TestClass>() { Chi3 };

            TestClassCollections.Add(fo);
            TestClassCollections.Add(fo2);
        }

        private void Tree2_Datas()
        {
            var green = new SolidColorBrush(Color.FromRgb(85, 177, 85));
            var red = new SolidColorBrush(Color.FromRgb(196, 62, 28));


            TreeViewLight item1 = new TreeViewLight() { Name = "TCH_2354665", LightOne = green, LightTwo =green };
        }
    }
}
