using CS.Database.Entity;
using Microsoft.Extensions.Logging;

namespace CSK.Core.Services
{
    /// <summary>
    /// 物料服务实现
    /// </summary>
    public class MaterialService : IMaterialService
    {
        private readonly ILogger<MaterialService> _logger;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        public MaterialService(ILogger<MaterialService> logger)
        {
            _logger = logger;
        }
        
        /// <summary>
        /// 处理物料动态数据
        /// </summary>
        /// <param name="material">物料记录</param>
        public void HandleMaterialDynamicData(MaterialRecord material)
        {
            // 这里可以添加业务逻辑处理
            _logger.LogInformation($"处理物料数据: MaterialNumber={material.MaterialNumber}, Batch={material.Batch}");
        }
    }
}
