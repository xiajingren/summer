using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Summer.Domain.SeedWork
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
        Task<T> FirstAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

        Task<T> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

        Task<T> SingleAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

        Task<T> SingleOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    }
}