using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Summer.Domain.SeedWork;

namespace Summer.Infrastructure.MultiTenancy
{
    public class TenantDbSeed : IDataSeed
    {
        private readonly TenantDbContext _dbContext;

        public TenantDbSeed(TenantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
            await _dbContext.Database.MigrateAsync();

            if (await _dbContext.Tenants.AnyAsync())
            {
                return;
            }

            await _dbContext.Tenants.AddAsync(Tenant.Default);
            await _dbContext.SaveChangesAsync();
        }
    }
}