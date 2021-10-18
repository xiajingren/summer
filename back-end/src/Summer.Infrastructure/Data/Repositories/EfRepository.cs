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
        public SummerDbContext Context { get; } // todo:

        public EfRepository(SummerDbContext dbContext) : base(dbContext)
        {
            Context = dbContext;
        }

        // public EfRepository(DbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext,
        //     specificationEvaluator)
        // {
        // }

        public async Task<T> FirstAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstAsync(cancellationToken);
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T> SingleAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.SingleAsync(cancellationToken);
        }

        public async Task<T> SingleOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.SingleOrDefaultAsync(cancellationToken);
        }
    }
}