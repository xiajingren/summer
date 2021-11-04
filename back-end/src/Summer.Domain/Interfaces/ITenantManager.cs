using System.Threading.Tasks;
using Summer.Domain.Entities;

namespace Summer.Domain.Interfaces
{
    public interface ITenantManager
    {
        Task<Tenant> CreateAsync(string code, string name, string connectionString, string host);

        Task UpdateAsync(Tenant tenant, string code, string name, string connectionString, string host);
    }
}