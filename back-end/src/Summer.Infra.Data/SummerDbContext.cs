using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Summer.Domain.SeedWork;
using Summer.Infra.Data.SeedWork;

namespace Summer.Infra.Data
{
    public class SummerDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public SummerDbContext(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.PublishDomainEventsAsync(this);

            var result = await SaveChangesAsync(cancellationToken) > 0;

            return result;
        }
    }
}