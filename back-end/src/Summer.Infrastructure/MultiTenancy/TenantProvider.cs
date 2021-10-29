using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Summer.Infrastructure.MultiTenancy
{
    public class TenantProvider : ITenantProvider
    {
        private readonly TenantDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(TenantDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Tenant> GetTenantAsync()
        {
            try
            {
                var tenantCode = GetTenantCode();

                var tenant = await _dbContext.Tenants.SingleOrDefaultAsync(x => x.TenantCode == tenantCode);
                return tenant ?? Tenant.Default;
            }
            catch
            {
                return Tenant.Default;
            }
        }

        private string GetTenantCode()
        {
            if (_httpContextAccessor?.HttpContext == null)
            {
                return "Default";
            }

            if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Tenant"))
            {
                return _httpContextAccessor.HttpContext.Request.Headers["Tenant"].ToString();
            }

            if (_httpContextAccessor.HttpContext.Request.Query.ContainsKey("Tenant"))
            {
                return _httpContextAccessor.HttpContext.Request.Query["Tenant"].ToString();
            }

            // if (_httpContextAccessor.HttpContext.Request.Host.HasValue)
            // {
            //     return _httpContextAccessor.HttpContext.Request.Host.Host;
            // }

            return "Default";
        }
    }
}