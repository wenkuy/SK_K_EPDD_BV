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

        [ObservableProperty]
        ObservableCollection<TreeViewYear> treeViewYeads = new ObservableCollection<TreeViewYear>();

        public SettingPageVm()
        {
            Tree1_Datas();
            Tree2_Datas();
            TreeView3();
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

            SolidColorBrush green = new SolidColorBrush(Color.FromRgb(85, 177, 85));
            SolidColorBrush red = new SolidColorBrush(Color.FromRgb(196, 62, 28));
            SolidColorBrush yellow = new SolidColorBrush(Color.FromRgb(220, 220, 170));
            TreeViewLight item1 = new TreeViewLight() { Name = "TCH_2354665", LightOne = green, LightTwo = green };
            TreeViewLight item2 = new TreeViewLight() { Name = "TCH_dsfsg", LightOne = green, LightTwo = red };
            TreeViewLight item3 = new TreeViewLight() { Name = "TCH_uyk6eg", LightOne = green, LightTwo = yellow };

            TreeViewLightCollections.Add(item1);
            TreeViewLightCollections.Add(item2);
            TreeViewLightCollections.Add(item3);
        }

        private void TreeView3()
        {
            SolidColorBrush green = new SolidColorBrush(Color.FromRgb(85, 177, 85));
            SolidColorBrush red = new SolidColorBrush(Color.FromRgb(196, 62, 28));
            SolidColorBrush yellow = new SolidColorBrush(Color.FromRgb(220, 220, 170));

            TreeViewLight item1 = new TreeViewLight() { Name = "TCH_2354665", LightOne = green, LightTwo = green };
            TreeViewLight item2 = new TreeViewLight() { Name = "TCH_dsfsg", LightOne = green, LightTwo = red };
            TreeViewLight item3 = new TreeViewLight() { Name = "TCH_uyk6eg", LightOne = green, LightTwo = yellow };
            List<TreeViewLight> k1 = new List<TreeViewLight>();
            List<TreeViewLight> k2 = new List<TreeViewLight>();
            k1.Add(item1);
            k1.Add(item2);
            k2.Add(item3);  

            TreeViewDay d1 = new TreeViewDay() { Day = "9日", Files = k1 };
            TreeViewDay d2 = new TreeViewDay() { Day = "10日", Files = k2 };

            TreeViewMonth M = new TreeViewMonth() { Month = "11月", Days = new List<TreeViewDay>() { d1, d2 } };

            TreeViewYear Y = new TreeViewYear() { Year = "2026年", Months = new List<TreeViewMonth>() { M } };

            TreeViewYeads.Add(Y);
        }
    }
}
