using System;
using System.Threading;
using System.Threading.Tasks;

namespace Summer.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    }
}