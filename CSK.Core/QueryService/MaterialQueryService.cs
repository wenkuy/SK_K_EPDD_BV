using CS.Database;
using CS.Database.Entity;
using System.Collections.Generic;
using System.Linq;

namespace CSK.Core.QueryService
{
    public interface IMaterialQueryService
    {
        MaterialRecord[] GetMaterialRecords(string materialNumber);
        Dictionary<string, int> GetMaterialUsageStatistics(float hours = 12);
    }

    public class MaterialQueryService : IMaterialQueryService
    {
        private readonly IDBRecordPOperation _dbOperation;

        public MaterialQueryService(IDBRecordPOperation dbOperation)
        {
            _dbOperation = dbOperation;
        }

        public MaterialRecord[] GetMaterialRecords(string materialNumber)
        {
            if (string.IsNullOrEmpty(materialNumber))
            {
                return _dbOperation.DBGetObjs<MaterialRecord>(e => true);
            }
            return _dbOperation.DBGetObjs<MaterialRecord>(e => e.MaterialNumber == materialNumber);
        }

        public Dictionary<string, int> GetMaterialUsageStatistics(float hours = 12)
        {
            var records = GetMaterialRecords(string.Empty);

            if (records == null || records.Length == 0)
            {
                return new Dictionary<string, int>();
            }

            var statistics = records
                .GroupBy(e => e.MaterialNumber)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );

            return statistics;
        }
    }
}