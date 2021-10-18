using System.Threading.Tasks;
using Summer.Domain.Results;

namespace Summer.Domain.Interfaces
{
    public interface IIdentityService
    {
        Task<TokenResult> RegisterAsync(string username, string password);

        Task<TokenResult> LoginAsync(string username, string password);

        Task<TokenResult> RefreshTokenAsync(string token, string refreshToken);
    }
}