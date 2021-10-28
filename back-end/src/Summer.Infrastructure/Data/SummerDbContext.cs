using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
        private readonly Tenant _tenant;
        private readonly string _connectionString;

        public SummerDbContext(IMediator mediator, ITenantProvider tenantProvider, IConfiguration configuration)
        {
            _mediator = mediator;
            _tenant = tenantProvider.GetTenantAsync().Result;

            _connectionString = _tenant.ConnectionString ?? configuration.GetConnectionString("Default");
        }

        public SummerDbContext(IMediator mediator, string connectionString)
        {
            _mediator = mediator;
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
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

    public class SummerDbContextDesignFactory : IDesignTimeDbContextFactory<SummerDbContext>
    {
        public SummerDbContext CreateDbContext(string[] args)
        {
            return new SummerDbContext(new NoMediator(), "DataSource=app.db; Cache=Shared");
        }

        class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification,
                CancellationToken cancellationToken = default) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
                CancellationToken cancellationToken = default)
            {
                return Task.FromResult<TResponse>(default);
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}