using System;
using System.Threading;
using System.Threading.Tasks;

namespace Summer.Application.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginAsync(CancellationToken cancellationToken = default);
        
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}