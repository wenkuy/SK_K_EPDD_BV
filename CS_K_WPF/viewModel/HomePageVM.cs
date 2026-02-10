using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CS.Database;
using CS.Database.Entity;
using CSK.Core;
using Microsoft.Extensions.DependencyInjection;
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
            //【方式1】这种方式少用，不规范
            //Application.Current.Dispatcher.Invoke(() => ProductsDynamicData(e));
            //Application.Current.Dispatcher.BeginInvoke(() => ProductsDynamicData(e));

            //【方式2】ok
            CommonDispatcherHelper.BeginExecuteOnUiThread(() => { ProductsDynamicData(e); });

            //【方式3】ok
            //DispacherHelper.ExecuteOnUiThread(() => { DynamicData(e); });

            //【方式4】 这个是利用Binding订阅OnllectionChanged触发，Binding自己在会去检查线程上下文，并做出切换线程的处理。利用的就是Binding的辅助功能。
            //因为Binding在订阅的时候，会记住当前的线程上下文，所以后续触发CollectionChanged事件时，Binding会帮我们切换到正确的线程上下文去处理UI更新。
            //lock (_LockOnProducts)
            //{
            //    ProductsDynamicData(e);
            //}
            /*失败原因：当你的 Products 绑定到 DataGrid、ListView 这类控件时，WPF 会自动为它创建一个 CollectionView（用来管理排序、过滤、分组等 UI 显示逻辑）。
            但是CollectionView 是线程敏感的，它要求所有对源集合（Products）的修改必须在 UI 线程执行。但是Binding 把「处理 CollectionChanged 事件、更新 UI 控件」
            的逻辑切换到了 UI 线程，但「修改源集合（Products.Add/Products[i] = mes）」这个操作本身，依然在后台线程执行。所以失败！！！*/

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

        [ObservableProperty]
        private ObservableCollection<LineProductionRecord> lineProductionRecordColloections = new ObservableCollection<LineProductionRecord>();

        private IDBRecordPOperation _dbOperation;

        public HomePageVM(IServiceProvider serviceProvider, IDBRecordPOperation dbOperation)
        {
            _dbOperation = dbOperation;

            this.serviceProvider = serviceProvider;
            ButtonClickCMD = new RelayCommand(async () =>
            {
                //统计过去2小时的数据
                LineProductionRecord r = await Task.Run<LineProductionRecord>(() => StatisticalData(2));
                LineProductionRecordColloections.Add(r);
            });

            //注册：接受来自CS.Communication模块发来的UDP数据(已经根据协议去掉了头的content---byte[])
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


        /// <summary>
        /// 统计time内的数据
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="hours">按时长计算</param>
        /// <returns></returns>
        private LineProductionRecord StatisticalData(float hours = 12)
        {
            // 1.获取time时间内的记录
            //ProducttProductionRecord[] productRocords = ServiceProvider.DatabaseServicesProvider().DBGetObjs<ProducttProductionRecord>(
            //   e => StrTimeToDateTime.StrToDateTIme(e.ProductTime) >= startTime && StrTimeToDateTime.StrToDateTIme(e.ProductTime) <= startTime.AddHours(hours));

            //1. 按小时 -天 -周 -月 -年统计
            string strtime = _dbOperation.DBGetSingleObj<ProducttProductionRecord>(e => true).ProductTime;
            DateTime time = StrTimeToDateTime.StrToDateTime(strtime);

            //ProducttProductionRecord[] productRocords = ServiceProvider.DatabaseServicesProvider().DBGetObjs<ProducttProductionRecord>(
            //  e => time <= StrTimeToDateTime.StrToDateTime(e.ProductTime) && StrTimeToDateTime.StrToDateTime(e.ProductTime) <= time.AddHours(hours));
            ProducttProductionRecord[] productRocords = _dbOperation.DBGetObjs<ProducttProductionRecord>(
              e => e.ProductNumber == "A-XH-GHTY0215");


            // 2.统计分析
            // 2.1统计time内产量 合格率  故障率
            int goods = 0;
            for (int i = 0; i < productRocords.Length; i++)
            {
                if (true == productRocords[i].QualityInspectionResult)
                {
                    goods++;
                }
            }

            //删除测试
            _dbOperation.DBDeleteObj<ProducttProductionRecord>(e => e.Param.Temperature > 20.4f);

            //修改测试
            _dbOperation.EditObj<ProducttProductionRecord>(e => e.Param.Temperature == 20.1f, e => e.SetProperty(p => p.Param.Humidity, 99.5f));

            return new LineProductionRecord() { ProductsRate = new CS.Database.Structs.ProductQualifiedRate() { Product1Rate = 0.5f } };
        }
    }
}
