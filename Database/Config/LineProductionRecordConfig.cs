using Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Config
{
    public class LineProductionRecordConfig: IEntityTypeConfiguration<LineProductionRecord>
    {
        public void Configure(EntityTypeBuilder<LineProductionRecord> builder)
        {
            builder.ToTable("T_LineProductionRecords");
        }
    }
}
