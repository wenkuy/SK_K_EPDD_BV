using CS.Database.Entity;
using CSK.Core.QueryService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSK.Core.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialQueryService _materialQueryService;
        private readonly ILogger<MaterialService> _logger;

        public MaterialService(IMaterialQueryService materialQueryService, ILogger<MaterialService> logger)
        {
            _materialQueryService = materialQueryService;
            _logger = logger;
        }

        public void HandleMaterialDynamicData(MaterialRecord material)
        {
            _logger.LogInformation($"处理物料数据: MaterialNumber={material.MaterialNumber}, Batch={material.Batch}");
        }

        public Dictionary<string, int> GetMaterialUsageStatistics(float hours = 12)
        {
            return _materialQueryService.GetMaterialUsageStatistics(hours);
        }
    }
}