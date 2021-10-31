using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Summer.Application.Interfaces;
using Summer.Application.Responses;
using Summer.Infrastructure.MasterData;

namespace Summer.Infrastructure.Services
{
    public class TenantService : ITenantService
    {
        private readonly MasterDbContext _dbContext;

        public TenantService(MasterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TenantResponse> GetByIdAsync(int id)
        {
            var tenant = await _dbContext.Tenants.SingleOrDefaultAsync(x => x.Id == id);
            return tenant.ToResponse();
        }

        public async Task<TenantResponse> GetByCodeAsync(string code)
        {
            var tenant = await _dbContext.Tenants.SingleOrDefaultAsync(x => x.Code == code);
            return tenant.ToResponse();
        }

        public async Task<TenantResponse> GetByHostAsync(string host)
        {
            var tenant = await _dbContext.Tenants.SingleOrDefaultAsync(x => x.Host == host);
            return tenant.ToResponse();
        }
    }
}