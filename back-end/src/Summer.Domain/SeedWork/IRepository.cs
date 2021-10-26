using Ardalis.Specification;

namespace Summer.Domain.SeedWork
{
    public interface IRepository<T> : IReadRepository<T>, IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}