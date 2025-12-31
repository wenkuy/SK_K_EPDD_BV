using Database.Config;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Database
{
    public class CSK_DBContext : DbContext
    {
        public DbSet<ProducttProductionRecord> ProducttProductionRecords { get; set; }
        public DbSet<MaterialRecord> MaterialRecordConfigs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string str = "Server=.;Database=CSK_WPF;Trusted_Connection=True;MultipleActiveResultSets=true";
            string str = "Server=.;Database=CSK_WPF;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(str);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CSK_DBContext).Assembly);


            //// 关键：配置 ProducttProductionRecord 拥有 ProductEnvironmentPraram 类
            //// 类类型无需额外处理，EF Core 6.x 完美支持
            //modelBuilder.Entity<ProducttProductionRecord>()
            //    .OwnsOne(p => p.Param);

            // 核心一行：配置Param的字段映射（解决数据存不进的关键）
            modelBuilder.Entity<ProducttProductionRecord>().OwnsOne(p => p.Param, p =>
            {
                p.Property(x => x.Temperature).HasColumnName("Param_Temperature");
                p.Property(x => x.Humidity).HasColumnName("Param_Humidity");
            });
            modelBuilder.Entity<MaterialRecord>().OwnsOne(p => p.QualityPArams);
        }
    }
}
