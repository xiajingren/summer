using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Summer.Infrastructure.MultiTenancy
{
    public class TenantDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public TenantDbContext(DbContextOptions<TenantDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            var connectionString = _configuration.GetConnectionString("Tenant") ??
                                   _configuration.GetConnectionString("Default");

            optionsBuilder.UseSqlite(connectionString);

            Database.MigrateAsync().Wait();
        }

        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>(b =>
            {
                b.ToTable("Tenants");

                b.HasKey(x => x.Id);
                b.Property(x => x.TenantCode).HasMaxLength(64).IsRequired();
                b.Property(x => x.TenantName).HasMaxLength(64).IsRequired();
                b.Property(x => x.ConnectionString).HasMaxLength(256);

                b.HasIndex(x => x.TenantCode).IsUnique();

                b.HasData(new Tenant[]
                {
                    new Tenant("Default", "默认租户")
                });
            });
        }
    }
}