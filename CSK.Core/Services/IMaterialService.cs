using CS.Database.Entity;

namespace CSK.Core.Services
{
    /// <summary>
    /// 物料服务接口
    /// </summary>
    public interface IMaterialService
    {
        /// <summary>
        /// 处理物料动态数据
        /// </summary>
        /// <param name="material">物料记录</param>
        void HandleMaterialDynamicData(MaterialRecord material);
    }
}
