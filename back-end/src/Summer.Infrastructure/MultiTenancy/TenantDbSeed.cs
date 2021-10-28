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

            var defaultTenant = new Tenant("Default", "默认租户");

            await _dbContext.Tenants.AddAsync(defaultTenant);
        }
    }
}