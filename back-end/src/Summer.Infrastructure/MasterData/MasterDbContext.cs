using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Summer.Infrastructure.MasterData
{
    public class MasterDbContext : DbContext
    {
        private readonly string _connectionString;

        public MasterDbContext(IConfiguration configuration)
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
                b.Property(x => x.Code).HasMaxLength(64).IsRequired();
                b.Property(x => x.Name).HasMaxLength(64).IsRequired();
                b.Property(x => x.ConnectionString).HasMaxLength(256);
                b.Property(x => x.Host).HasMaxLength(128);

                b.HasIndex(x => x.Code).IsUnique();
                b.HasIndex(x => x.Host).IsUnique();
            });
        }
    }
}