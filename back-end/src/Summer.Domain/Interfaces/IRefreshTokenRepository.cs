using System.Threading.Tasks;
using Summer.Domain.Entities;
using Summer.Domain.SeedWork;

namespace Summer.Domain.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> GetByTokenAsync(string token);
    }
}