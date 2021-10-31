using System.Threading.Tasks;
using Summer.Application.Responses;

namespace Summer.Application.Interfaces
{
    public interface ITenantService
    {
        Task<TenantResponse> GetByIdAsync(int id);

        Task<TenantResponse> GetByCodeAsync(string code);

        Task<TenantResponse> GetByHostAsync(string host);
    }
}