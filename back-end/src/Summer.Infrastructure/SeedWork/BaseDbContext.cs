using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Summer.Infrastructure.Extensions;

namespace Summer.Infrastructure.SeedWork
{
    public abstract class BaseDbContext : DbContext
    {
        private readonly IMediator _mediator;

        protected abstract string ConnectionString { get; }

        public BaseDbContext(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
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

        protected virtual void DetectChanges()
        {
        }
    }
}