using CS.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Config
{
    public class MaterialRecordConfig : IEntityTypeConfiguration<MaterialRecord>
    {
        public void Configure(EntityTypeBuilder<MaterialRecord> builder)
        {
            builder.ToTable("T_MaterialRecords");
        }
    }
}
