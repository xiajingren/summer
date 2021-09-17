using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Summer.Domain.SeedWork;
using Summer.Infrastructure.SeedWork;

namespace Summer.Infrastructure.Data
{
    public class SummerContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public SummerContext(IMediator mediator)
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