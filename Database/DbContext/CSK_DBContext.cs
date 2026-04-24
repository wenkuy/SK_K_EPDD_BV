using CS.Database.Config;
using CS.Database.Entity;
using CS.Database.Structs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CS.Database
{
    public class CSK_DBContext : DbContext
    {
        public DbSet<ProducttProductionRecord> ProducttProductionRecords { get; set; }
        public DbSet<MaterialRecord> MaterialRecordConfigs { get; set; }
        public DbSet<LineProductionRecord> LineProductionRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //这是2019sql
            //string str = "Server=.;CS.Database=CSK_WPF;Trusted_Connection=True;MultipleActiveResultSets=true";
            //string str = "Server=.;Database=CSK_WPF;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

            //2022sql
            string str = "Server=localhost\\SQLEXPRESS;Database=CSK_WPF;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(str);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CSK_DBContext).Assembly);

            //方式1
            modelBuilder.Entity<MaterialRecord>().OwnsOne(p => p.QualityPArams);

            //方式2
            modelBuilder.Entity<ProducttProductionRecord>().OwnsOne(p => p.Param, p =>
            {
                p.Property(x => x.Temperature).HasColumnName("Param_Temperature");
                p.Property(x => x.Humidity).HasColumnName("Param_Humidity");
            });

            // 主实体直接映射结构体字段（EF自动拆分到主表）
            modelBuilder.Entity<LineProductionRecord>().OwnsOne(p => p.ProductsOutput);
            modelBuilder.Entity<LineProductionRecord>().OwnsOne(p => p.ProductsRate);
        }
    }
}
