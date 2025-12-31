using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Config
{
    public class ProducttProductionRecordConfig : IEntityTypeConfiguration<ProducttProductionRecord>
    {
        public void Configure(EntityTypeBuilder<ProducttProductionRecord> builder)
        {
            builder.ToTable("T_ProducttProductionRecords");
        }
    }
}
