using Microsoft.EntityFrameworkCore;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Repository.Interfaces;

namespace Hayalpc.Fatura.Data
{
    public class HpDbContext : DbContext
    {
        public HpDbContext(DbContextOptions<HpDbContext> options) : base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.LazyLoadingEnabled = false;
            this.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
        }

      

    }
}
