using BaseProj;
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
using System.Windows;
using System.Windows.Threading;

namespace CS_K_WPF.viewModel
{
    public partial class HomePageVM : ObservableObject
    {
        [ObservableProperty]
        private ProducttProductionRecord realtimeProduct = new();

        [ObservableProperty]
        private EquipmentStateRecord equipmentUDPInfo = new();

        [ObservableProperty]
        private MaterialRecord materialUDPInfo = new();



        partial void OnRealtimeProductChanged(ProducttProductionRecord e)
        {
            //【方式1】这种方式少用，不规范
            //Application.Current.Dispatcher.Invoke(() => DynamicData(e));

            //【方式2】
            CommonDispatcherHelper.ExecuteOnUiThread(() => {ProductsDynamicData(e);});

            //【方式3】
            //DispacherHelper.ExecuteOnUiThread(() => { DynamicData(e); });
        }

        partial void OnEquipmentUDPInfoChanged(EquipmentStateRecord e)
        {
            //处理设备状态变更的逻辑
            CommonDispatcherHelper.ExecuteOnUiThread(() =>
            {
                EquipmentStateDynamicData(e);
            });

        }

        partial void OnMaterialUDPInfoChanged(MaterialRecord e)
        {
            //处理物料信息变更的逻辑
            CommonDispatcherHelper.ExecuteOnUiThread(() =>
            {
                MaterialDynamicData(e);
            });
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
        private ObservableCollection<EquipmentStateRecord> equipmentStateRecordsCollections = new ObservableCollection<EquipmentStateRecord>();

        [ObservableProperty]
        private ObservableCollection<MaterialRecord> materialsInfoColloections = new ObservableCollection<MaterialRecord>();


        public HomePageVM()
        {
         
        }

        public void Udp_ProductDates(ProducttProductionRecord e1)
        {
            RealtimeProduct = e1;
        }
        public void Udp_MaterialDates(MaterialRecord e1)
        {
            MaterialUDPInfo = e1;
        }

        public void Udp_EquipmentRecodeDates(EquipmentStateRecord e1)
        {
            EquipmentUDPInfo = e1;
        }


        private void ProductsDynamicData(ProducttProductionRecord mes)
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

        private void EquipmentStateDynamicData(EquipmentStateRecord mes)
        {
            if (!EquipmentStateRecordsCollections.Any(e => e.EquipmentNumber == mes.EquipmentNumber)) //不包含就添加
            {
                EquipmentStateRecordsCollections.Add(mes);
            }
            else
            {
                for (int i = 0; i < EquipmentStateRecordsCollections.Count; i++)
                {
                    if (EquipmentStateRecordsCollections[i].EquipmentNumber == mes.EquipmentNumber)
                    {
                        EquipmentStateRecordsCollections[i] = mes;
                        break;
                    }
                }
            }
        }

        private void MaterialDynamicData(MaterialRecord mes)
        {
            if (!MaterialsInfoColloections.Any(e => e.MaterialNumber == mes.MaterialNumber)) //不包含就添加
            {
                MaterialsInfoColloections.Add(mes);
            }
            else
            {
                for (int i = 0; i < MaterialsInfoColloections.Count; i++)
                {
                    if (MaterialsInfoColloections[i].MaterialNumber == mes.MaterialNumber)
                    {
                        MaterialsInfoColloections[i] = mes;
                        break;
                    }
                }
            }
        }
    }
}
