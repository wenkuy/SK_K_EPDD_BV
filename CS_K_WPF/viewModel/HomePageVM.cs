using CommunityToolkit.Mvvm.ComponentModel;
using CS_K_WPF.Base.Enum;
using CS_K_WPF.model;
using CS_K_WPF.Structs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.viewModel
{
    public partial class HomePageVM : ObservableObject
    {

        /// <summary>
        /// 商品生产实时信息
        /// </summary>
        public ObservableCollection<RealtimeProductInfo> Products
        {
            get { return products; }
            set { SetProperty(ref products, value); }
        }
        private ObservableCollection<RealtimeProductInfo> products = new ObservableCollection<RealtimeProductInfo>();



        //自动生成属性
        [ObservableProperty]
        private ObservableCollection<EquipmentStateRecord> equipmentStateRecords = new ObservableCollection<EquipmentStateRecord>();



        public HomePageVM()
        {
            RealtimeProductInfo k1 = new RealtimeProductInfo();
            k1.EquipmentNumber = "ajbadh";
            k1.ProductNumber = "1564516545";
            k1.Param = new ProductEnvironmentPraram() { Temperature = 20, Humidity = 11.5f };
            Products.Add(k1);

            RealtimeProductInfo k2 = new RealtimeProductInfo();
            k2.EquipmentNumber = "aj----badh";
            k2.ProductNumber = "236";
            k2.Param = new ProductEnvironmentPraram() { Temperature = 21.5f, Humidity = 10.5f };
            Products.Add(k2);

            EquipmentStateRecord e1 = new EquipmentStateRecord();
            e1.EquipmentState = EEquipmentState.Running;
            e1.EquipmentNumber = "AK001";
            EquipmentStateRecords.Add(e1);

            EquipmentStateRecord e2 = new EquipmentStateRecord();
            e2.EquipmentState = EEquipmentState.Running;
            e2.EquipmentNumber = "AK003";
            EquipmentStateRecords.Add(e2);
        }

    }
}
