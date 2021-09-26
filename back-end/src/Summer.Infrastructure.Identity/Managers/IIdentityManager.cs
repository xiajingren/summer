using System.Threading.Tasks;
using Summer.Infrastructure.Identity.Dtos;

namespace Summer.Infrastructure.Identity.Managers
{
    public interface IIdentityManager
    {
        Task<TokenOutput> RegisterAsync(string username, string password);

        Task<TokenOutput> LoginAsync(string username, string password);

        Task<TokenOutput> RefreshTokenAsync(string token, string refreshToken);
    }
}