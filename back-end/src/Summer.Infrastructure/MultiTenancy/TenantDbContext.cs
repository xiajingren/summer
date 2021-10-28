using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Summer.Infrastructure.MultiTenancy
{
    public class TenantDbContext : DbContext
    {
        private readonly string _connectionString;

        public TenantDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Tenant") ??
                                configuration.GetConnectionString("Default");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>(b =>
            {
                b.ToTable("Tenants");

                b.HasKey(x => x.Id);
                b.Property(x => x.TenantCode).HasMaxLength(64).IsRequired();
                b.Property(x => x.TenantName).HasMaxLength(64).IsRequired();
                b.Property(x => x.ConnectionString).HasMaxLength(256);

                b.HasIndex(x => x.TenantCode).IsUnique();
            });
        }
    }
}