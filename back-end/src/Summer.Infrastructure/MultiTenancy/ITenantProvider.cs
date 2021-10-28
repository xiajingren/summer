using System.Threading.Tasks;

namespace Summer.Infrastructure.MultiTenancy
{
    public interface ITenantProvider
    {
        Task<Tenant> GetTenantAsync();
    }
}