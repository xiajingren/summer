using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Summer.Domain.SeedWork;

namespace Summer.Infrastructure.MasterData
{
    public class MasterDbSeed : IDataSeed
    {
        private readonly MasterDbContext _dbContext;

        public MasterDbSeed(MasterDbContext dbContext)
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