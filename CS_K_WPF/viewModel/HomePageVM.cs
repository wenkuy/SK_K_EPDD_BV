using CommunityToolkit.Mvvm.ComponentModel;
using CS_K_WPF.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.viewModel
{
    public class HomePageVM : ObservableObject
    {

        /// <summary>
        /// 商品生产实时信息
        /// </summary>
        public RealtimeProductInfo RealtimeProductInfo
        {
            get { return realtimeProductInfo; }
            set { SetProperty(ref realtimeProductInfo, value); }
        }
        private RealtimeProductInfo realtimeProductInfo = null;


        private ObservableCollection<RealtimeProductInfo> products = new ObservableCollection<RealtimeProductInfo>();
        public ObservableCollection<RealtimeProductInfo> Products
        {
            get { return products; }
            set { SetProperty(ref products, value); }
        }

        public HomePageVM()
        {
            RealtimeProductInfo k1 = new RealtimeProductInfo();
            k1.EquipmentNumber = "ajbadh";
            k1.ProductNumber = "1564516545";
            RealtimeProductInfo k2 = new RealtimeProductInfo();
            k1.EquipmentNumber = "aj----badh";
            k1.ProductNumber = "236";
            Products.Add(k1);
            Products.Add(k2);
        }

    }
}
