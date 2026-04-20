using CS.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Repository
{
    /// <summary>
    /// 物料仓储接口
    /// </summary>
    public interface IMaterialRepository : IRepository<MaterialRecord>
    {
        /// <summary>
        /// 获取物料记录
        /// </summary>
        /// <param name="materialNumber">物料编号</param>
        /// <returns></returns>
        MaterialRecord[] GetMaterialRecords(string materialNumber);
    }
}