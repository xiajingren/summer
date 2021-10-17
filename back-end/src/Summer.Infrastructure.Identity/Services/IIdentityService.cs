using System.Threading.Tasks;
using Summer.Infrastructure.Identity.Dtos;

namespace Summer.Infrastructure.Identity.Services
{
    public interface IIdentityService
    {
        Task<TokenOutputDto> RegisterAsync(string username, string password);

        Task<TokenOutputDto> LoginAsync(string username, string password);

        Task<TokenOutputDto> RefreshTokenAsync(string token, string refreshToken);
    }
}