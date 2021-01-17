using Microsoft.EntityFrameworkCore;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Library.Repository.Interfaces;

namespace Hayalpc.Fatura.Data
{
    public class HpDbContext : DbContext
    {
        public HpDbContext()
        {
        }

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

        public DbSet<BlobFile> BlobFile { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Dealer> Dealer { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Institution> Institution { get; set; }
        public DbSet<InvoicePayment> InvoicePayment { get; set; }
        public DbSet<ResetPassword> ResetPassword { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<TableDefinition> TableDefinition { get; set; }
        public DbSet<TableHistory> TableHistory { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserBulletin> UserBulletin { get; set; }
        public DbSet<UserLog> UserLog { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
    }
}
