using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Summer.Application.UnitOfWork;
using Summer.Infrastructure.Data;
using Summer.Infrastructure.MasterData;

namespace Summer.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;

        private readonly SummerDbContext _summerDbContext;
        private readonly MasterDbContext _masterDbContext;

        public UnitOfWork(SummerDbContext summerDbContext, MasterDbContext masterDbContext)
        {
            _summerDbContext = summerDbContext;
            _masterDbContext = masterDbContext;
        }

        public async Task BeginAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _summerDbContext.Database.BeginTransactionAsync(cancellationToken);
            await _masterDbContext.Database.UseTransactionAsync(_transaction.GetDbTransaction(), cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.CommitAsync(cancellationToken);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}