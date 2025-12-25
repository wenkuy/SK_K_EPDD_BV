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
        [ObservableProperty]
        private ProducttProductionRecord realtimeProduct = new();

        partial void OnRealtimeProductChanged(ProducttProductionRecord e)
        {
            DynamicData(e);
        }



        /// <summary>
        /// 商品生产实时信息
        /// </summary>
        public ObservableCollection<ProducttProductionRecord> Products
        {
            get { return products; }
            set
            {
                SetProperty(ref products, value);
            }
        }
        private ObservableCollection<ProducttProductionRecord> products = new ObservableCollection<ProducttProductionRecord>();



        //自动生成属性
        [ObservableProperty]
        private ObservableCollection<EquipmentStateRecord> equipmentStateRecords = new ObservableCollection<EquipmentStateRecord>();



        public HomePageVM()
        {
            EquipmentStateRecord e1 = new EquipmentStateRecord();
            e1.EquipmentState = EEquipmentState.Running;
            e1.EquipmentNumber = "AK001";
            EquipmentStateRecords.Add(e1);
        }

        public void GetMesData(ProducttProductionRecord e1)
        {
            RealtimeProduct = e1;
        }

        private void DynamicData(ProducttProductionRecord mes)
        {
            if (!Products.Any(e => e.ProductNumber == mes.ProductNumber)) //不包含就添加
            {
                Products.Add(mes);
            }
            else
            {
                for (int i = 0; i < Products.Count; i++)
                {
                    if (Products[i].ProductNumber == mes.ProductNumber)
                    {
                        Products[i] = mes;
                        break;
                    }
                }
            }
        }
    }
}
