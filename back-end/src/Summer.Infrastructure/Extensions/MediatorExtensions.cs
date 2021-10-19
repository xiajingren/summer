using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Summer.Domain.SeedWork;

namespace Summer.Infrastructure.Extensions
{
    public static class MediatorExtensions
    {
        /// <summary>
        /// 发布领域事件
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="context"></param>
        /// <typeparam name="TContext"></typeparam>
        public static async Task DispatchDomainEventsAsync<TContext>(this IMediator mediator, TContext context)
            where TContext : DbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent).ConfigureAwait(false);
        }
    }
}