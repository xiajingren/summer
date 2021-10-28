using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Summer.Domain.Entities;
using Summer.Domain.SeedWork;
using Summer.Infrastructure.Extensions;
using Summer.Infrastructure.MultiTenancy;

namespace Summer.Infrastructure.Data
{
    public class SummerDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly Tenant _tenant;

        public SummerDbContext(DbContextOptions<SummerDbContext> options, IMediator mediator,
            ITenantProvider tenantProvider, IConfiguration configuration) : base(options)
        {
            _mediator = mediator;
            _configuration = configuration;
            _tenant = tenantProvider.GetTenantAsync().Result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseSqlite(_tenant.HasSpecialDataBase
                ? _tenant.ConnectionString
                : _configuration.GetConnectionString("Default"));

            Database.MigrateAsync().Wait();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<BaseEntity>().Property<int>("TenantId").IsRequired();

            // Configure entity filters

            #region FilterConfiguration

            modelBuilder.Entity<BaseEntity>().HasQueryFilter(b => EF.Property<int>(b, "TenantId") == _tenant.Id);

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries<BaseEntity>().Where(
                e => e.State == EntityState.Added))
            {
                item.CurrentValues["TenantId"] = _tenant.Id;
            }

            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // dispatch events only if save was successful
            await _mediator.DispatchDomainEventsAsync(this);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}