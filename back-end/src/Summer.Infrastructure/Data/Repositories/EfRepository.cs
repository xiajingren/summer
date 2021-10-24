using System;
using Ardalis.Specification.EntityFrameworkCore;
using Summer.Domain.SeedWork;

namespace Summer.Infrastructure.Data.Repositories
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(SummerDbContext dbContext) : base(dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
        }

        // public EfRepository(DbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext,
        //     specificationEvaluator)
        // {
        // }
    }
}