using System.Threading.Tasks;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Domain.Services
{
    public class TenantManager : ITenantManager
    {
        private readonly IRepository<Tenant> _tenantRepository;

        public TenantManager(IRepository<Tenant> tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Tenant> CreateAsync(string code, string name, string connectionString, string host)
        {
            var storedTenant = await _tenantRepository.GetBySpecAsync(new TenantByCodeSpec(code));
            if (storedTenant != null)
            {
                throw new BusinessException("租户Code已存在");
            }

            if (!string.IsNullOrEmpty(host))
            {
                storedTenant = await _tenantRepository.GetBySpecAsync(new TenantByHostSpec(host));
                if (storedTenant != null)
                {
                    throw new BusinessException("租户Host已存在");
                }
            }

            var tenant = new Tenant(code, name, connectionString, host);

            return await _tenantRepository.AddAsync(tenant);
        }

        public async Task UpdateAsync(Tenant tenant, string code, string name, string connectionString, string host)
        {
            var storedTenant = await _tenantRepository.GetBySpecAsync(new TenantByCodeSpec(code));
            if (storedTenant != null && storedTenant.Id != tenant.Id)
            {
                throw new BusinessException("租户Code已存在");
            }

            if (!string.IsNullOrEmpty(host))
            {
                storedTenant = await _tenantRepository.GetBySpecAsync(new TenantByHostSpec(host));
                if (storedTenant != null && storedTenant.Id != tenant.Id)
                {
                    throw new BusinessException("租户Host已存在");
                }
            }

            tenant.Code = code;
            tenant.Name = name;
            tenant.ConnectionString = connectionString;
            tenant.Host = host;

            await _tenantRepository.UpdateAsync(tenant);
        }
    }
}