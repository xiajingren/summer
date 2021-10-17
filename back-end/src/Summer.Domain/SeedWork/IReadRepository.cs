using Ardalis.Specification;

namespace Summer.Domain.SeedWork
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}