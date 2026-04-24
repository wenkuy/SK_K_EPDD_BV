using CS.Database;
using CS.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSK.Core.QueryService
{
    public interface IProductQueryService
    {
        ProducttProductionRecord[] GetProductRecords(string productNumber);
        LineProductionRecord GetProductStatistics(string productNumber);
    }

    public class ProductQueryService : IProductQueryService
    {
        private readonly IDBRecordPOperation _dbOperation;

        public ProductQueryService(IDBRecordPOperation dbOperation)
        {
            _dbOperation = dbOperation;
        }

        public ProducttProductionRecord[] GetProductRecords(string productNumber)
        {
            if (string.IsNullOrEmpty(productNumber))
            {
                return _dbOperation.DBGetObjs<ProducttProductionRecord>(e => true);
            }
            return _dbOperation.DBGetObjs<ProducttProductionRecord>(e => e.ProductNumber == productNumber);
        }

        public LineProductionRecord GetProductStatistics(string productNumber)
        {
            var productRecords = GetProductRecords(productNumber);

            if (productRecords == null || productRecords.Length == 0)
            {
                return new LineProductionRecord();
            }

            int totalProducts = productRecords.Length;
            int goodProducts = productRecords.Count(e => e.QualityInspectionResult);
            float passRate = totalProducts > 0 ? (float)goodProducts / totalProducts : 0;

            var statistics = new LineProductionRecord
            {
                ProductsRate = new CS.Database.Structs.ProductQualifiedRate
                {
                    Product1Rate = passRate
                }
            };

            return statistics;
        }
    }
}