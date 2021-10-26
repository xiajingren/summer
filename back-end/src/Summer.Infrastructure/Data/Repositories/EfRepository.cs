using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Summer.Domain.SeedWork;

namespace Summer.Infrastructure.Data.Repositories
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        private readonly SummerDbContext _dbContext;

        public EfRepository(SummerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // public EfRepository(DbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext,
        //     specificationEvaluator)
        // {
        // }
        
        public virtual async Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification, true).AnyAsync(cancellationToken);
        }
        
        public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().AnyAsync(cancellationToken);
        }
    }
}