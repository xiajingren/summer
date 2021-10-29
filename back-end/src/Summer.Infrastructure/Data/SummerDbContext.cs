using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
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

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                FilterConfigurationMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        private static readonly MethodInfo FilterConfigurationMethodInfo
            = typeof(SummerDbContext)
                .GetMethod(
                    nameof(FilterConfiguration),
                    BindingFlags.Instance | BindingFlags.NonPublic
                );

        private void FilterConfiguration<T>(ModelBuilder modelBuilder, IMutableEntityType mutableEntityType)
            where T : class
        {
            Expression<Func<T, bool>> expression = null;

            if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
            {
                expression = e => EF.Property<int>(e, "TenantId") == _tenant.Id;
            }

            if (expression == null)
                return;

            modelBuilder.Entity(mutableEntityType.ClrType).HasQueryFilter(expression);
        }

        protected virtual Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1,
            Expression<Func<T, bool>> expression2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                {
                    return _newValue;
                }

                return base.Visit(node);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            DetectChanges();

            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // dispatch events only if save was successful
            await _mediator.DispatchDomainEventsAsync(this);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        private void DetectChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries<BaseEntity>().Where(
                e => e.State == EntityState.Added))
            {
                item.CurrentValues["TenantId"] = _tenant.Id;
            }
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