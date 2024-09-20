using Bahrin.Harbour.Data.DBCollections;
using Bahrin.Harbour.Model.AccountModel;
using Bahrin.Harbour.Model.ClientModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bahrin.Harbour.Data.DataContext
{
    public class BahrinHarbourContext : IdentityDbContext<ApplicationUser>
    {
        public BahrinHarbourContext(DbContextOptions<BahrinHarbourContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .Property(c => c.PropertyPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Client>()
                .Property(c => c.AvailedDiscount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
/*
 * 
 * Migration Run Commands
 * 
 *  Add-Migration <<MigrationName>> -Project Bahrin.Harbour.Data -StartupProject AdminBahrin.Harbour
    Update-Database -Project Bahrin.Harbour.Data -StartupProject AdminBahrin.Harbour
 */ 