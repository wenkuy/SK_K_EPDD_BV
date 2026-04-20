using CS.Database.Entity;
using CS.Database.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSK.Core.Services
{
    /// <summary>
    /// 物料服务实现
    /// </summary>
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly ILogger<MaterialService> _logger;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="materialRepository">物料仓储接口</param>
        /// <param name="logger">日志记录器</param>
        public MaterialService(IMaterialRepository materialRepository, ILogger<MaterialService> logger)
        {
            _materialRepository = materialRepository;
            _logger = logger;
        }
        
        /// <summary>
        /// 处理物料动态数据
        /// </summary>
        /// <param name="material">物料记录</param>
        public void HandleMaterialDynamicData(MaterialRecord material)
        {
            _logger.LogInformation($"处理物料数据: MaterialNumber={material.MaterialNumber}, Batch={material.Batch}");
        }
        
        /// <summary>
        /// 获取物料使用统计数据
        /// </summary>
        /// <param name="hours">统计时间范围（小时）</param>
        /// <returns>物料使用统计</returns>
        public System.Collections.Generic.Dictionary<string, int> GetMaterialUsageStatistics(float hours = 12)
        {
            // 通过仓储层获取原始数据
            var records = _materialRepository.GetMaterialRecords(string.Empty);

            if (records == null || records.Length == 0)
            {
                _logger.LogInformation("没有找到物料使用记录");
                return new Dictionary<string, int>();
            }

            // 在业务服务层进行统计分析
            var statistics = records
                .GroupBy(e => e.MaterialNumber)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );

            _logger.LogInformation($"物料使用统计完成: 物料数={statistics.Count}");
            return statistics;
        }
    }
}