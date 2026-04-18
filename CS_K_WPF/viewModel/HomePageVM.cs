﻿﻿﻿using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CS.Base;
using CS.Communication;
using CS.Database.Entity;
using CSK.Core;
using CSK.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
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

        private IServiceProvider serviceProvider;

        public RelayCommand ButtonClickCMD { get; }

        // 在 HomePageVM 类的开头添加
        private readonly object _LockOnProducts = new object();

        partial void OnRealtimeProductChanged(ProducttProductionRecord e)
        {
            // 调用业务服务处理数据
            _productService.HandleProductDynamicData(e);
            
            // 更新UI数据
            CommonDispatcherHelper.BeginExecuteOnUiThread(() => { ProductsDynamicData(e); });
        }

        partial void OnEquipmentUDPInfoChanged(EquipmentStateRecord e)
        {
            // 调用业务服务处理数据
            _equipmentService.HandleEquipmentStateDynamicData(e);
            
            // 更新UI数据
            CommonDispatcherHelper.ExecuteOnUiThread(() =>
            {
                EquipmentStateDynamicData(e);
            });

        }

        partial void OnMaterialUDPInfoChanged(MaterialRecord e)
        {
            // 调用业务服务处理数据
            _materialService.HandleMaterialDynamicData(e);
            
            // 更新UI数据
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

        [ObservableProperty]
        private ObservableCollection<LineProductionRecord> lineProductionRecordColloections = new ObservableCollection<LineProductionRecord>();

        private readonly IProductService _productService;
        private readonly IEquipmentService _equipmentService;
        private readonly IMaterialService _materialService;
        private readonly ILogger<HomePageVM> _log;
        
        public HomePageVM(IServiceProvider serviceProvider, 
                         IProductService productService, 
                         IEquipmentService equipmentService, 
                         IMaterialService materialService, 
                         ILogger<HomePageVM> logger) //Logger注入，记录日志
        {
            _productService = productService;
            _equipmentService = equipmentService;
            _materialService = materialService;
            _log = logger;

            this.serviceProvider = serviceProvider;
            ButtonClickCMD = new RelayCommand(async () =>
            {
                //统计过去2小时的数据
                LineProductionRecord r = await _productService.StatisticalData(2);
                LineProductionRecordColloections.Add(r);
                _log.LogInformation(""); //使用Log一次
            });

            //注册：接受来自CS.Communication模块发来的UDP数据(已经根据协议去掉了头的content---byte[]) -- 为了解耦采用该方式
            WeakReferenceMessenger.Default.Register<UdpDataForProducttProductionRecordMes>(this, (recipient, message) =>
            {
                RealtimeProduct = ProtocolParsing.Deserialize<ProducttProductionRecord>(message.Content);//字节流转成对象
                
            });
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

          
            // With this corrected line:
            _log.LogInformation($"ProductsDynamicData: ProductNumber={mes.ProductNumber}, TimeConsumed={mes.TimeConsumed}, QualityInspectionResult={mes.QualityInspectionResult}, ProductTime={mes.ProductTime}");
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
