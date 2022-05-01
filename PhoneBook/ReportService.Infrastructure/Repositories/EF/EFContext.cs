using Microsoft.EntityFrameworkCore;
using ReportService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Data.EF
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options) { }


        public DbSet<Report> Reports { get; set; }

        public DbSet<ReportDetail> ReportDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            //Tüm map dosyalarını configurasyona ekler.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            #region Tablo ve kolonların hepsinin isimlerini küçültür 
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetTableName();
                modelBuilder.Entity(entity.Name).ToTable(currentTableName.ToLower(new CultureInfo("en-US", false)));
            }

            modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties())
                .ToList()
                .ForEach(p => p.SetColumnName(p.Name.ToLower(new CultureInfo("en-US", false))));
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
