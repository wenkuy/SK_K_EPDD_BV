using CS.Database;
using CS.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSK.Core.QueryService
{
    public interface IEquipmentQueryService
    {
        EquipmentStateRecord[] GetEquipmentStateRecords(string equipmentNumber);
        Dictionary<string, int> GetEquipmentStateStatistics(float hours = 12);
    }

    public class EquipmentQueryService : IEquipmentQueryService
    {
        private readonly IDBRecordPOperation _dbOperation;

        public EquipmentQueryService(IDBRecordPOperation dbOperation)
        {
            _dbOperation = dbOperation;
        }

        public EquipmentStateRecord[] GetEquipmentStateRecords(string equipmentNumber)
        {
            if (string.IsNullOrEmpty(equipmentNumber))
            {
                return _dbOperation.DBGetObjs<EquipmentStateRecord>(e => true);
            }
            return _dbOperation.DBGetObjs<EquipmentStateRecord>(e => e.EquipmentNumber == equipmentNumber);
        }

        public Dictionary<string, int> GetEquipmentStateStatistics(float hours = 12)
        {
            var records = GetEquipmentStateRecords(string.Empty);

            if (records == null || records.Length == 0)
            {
                return new Dictionary<string, int>();
            }

            var statistics = records
                .GroupBy(e => e.EquipmentState)
                .ToDictionary(
                    g => g.Key.ToString(),
                    g => g.Count()
                );

            return statistics;
        }
    }
}