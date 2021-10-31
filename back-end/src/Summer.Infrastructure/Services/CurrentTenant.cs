using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Summer.Application.Interfaces;
using Summer.Infrastructure.MasterData;

namespace Summer.Infrastructure.Services
{
    public class CurrentTenant : ICurrentTenant
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUser _currentUser;
        private readonly MasterDbContext _dbContext;
        private readonly Tenant _tenant;

        public CurrentTenant(IHttpContextAccessor httpContextAccessor,
            ICurrentUser currentUser, MasterDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _currentUser = currentUser;
            _dbContext = dbContext;
            _tenant = GetTenantAsync().Result;
        }

        private async Task<Tenant> GetTenantAsync()
        {
            try
            {
                Tenant tenant;

                if (_currentUser.IsAuthenticated)
                {
                    tenant = await _dbContext.Tenants.SingleOrDefaultAsync(x => x.Id == _currentUser.TenantId);
                    if (tenant != null)
                    {
                        return tenant;
                    }
                }

                var tenantCode = GetTenantCode();
                if (!string.IsNullOrEmpty(tenantCode))
                {
                    tenant = await _dbContext.Tenants.SingleOrDefaultAsync(x => x.Code == tenantCode);
                    if (tenant != null)
                    {
                        return tenant;
                    }
                }

                var host = GetTenantHost();
                if (!string.IsNullOrEmpty(host))
                {
                    tenant = await _dbContext.Tenants.SingleOrDefaultAsync(x => x.Host == host);
                    if (tenant != null)
                    {
                        return tenant;
                    }
                }

                return Tenant.Default;
            }
            catch
            {
                return Tenant.Default;
            }
        }

        private string GetTenantHost()
        {
            if (_httpContextAccessor?.HttpContext == null)
            {
                return null;
            }

            if (_httpContextAccessor.HttpContext.Request.Host.HasValue)
            {
                return _httpContextAccessor.HttpContext.Request.Host.Value;
            }

            return null;
        }

        private string GetTenantCode()
        {
            if (_httpContextAccessor?.HttpContext == null)
            {
                return null;
            }

            if (_httpContextAccessor.HttpContext.Request.Query.ContainsKey("Tenant"))
            {
                return _httpContextAccessor.HttpContext.Request.Query["Tenant"].ToString();
            }

            if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Tenant"))
            {
                return _httpContextAccessor.HttpContext.Request.Headers["Tenant"].ToString();
            }

            return null;
        }

        public int Id => _tenant.Id;
        public string Code => _tenant.Code;
        public string Name => _tenant.Name;
        public string ConnectionString => _tenant.ConnectionString;
        public string Host => _tenant.Host;
    }
}