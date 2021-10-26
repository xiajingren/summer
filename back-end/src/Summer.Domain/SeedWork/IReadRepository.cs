using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Summer.Domain.SeedWork
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
        Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(CancellationToken cancellationToken = default);
    }
}