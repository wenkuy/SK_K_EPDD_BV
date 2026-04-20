using CS.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Repository
{
    /// <summary>
    /// 物料仓储实现
    /// </summary>
    public class MaterialRepository : Repository<MaterialRecord>, IMaterialRepository
    {
        private readonly ILogger<MaterialRepository> _logger;

        public MaterialRepository(CSK_DBContext dbContext, ILogger<MaterialRepository> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

        public MaterialRecord[] GetMaterialRecords(string materialNumber)
        {
            try
            {
                var records = GetAll(e => e.MaterialNumber == materialNumber);
                _logger.LogInformation($"获取物料记录完成: 物料编号={materialNumber}, 记录数={records?.Length ?? 0}");
                return records;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取物料记录失败: 物料编号={materialNumber}");
                return null;
            }
        }
    }
}