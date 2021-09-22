using System.Threading.Tasks;
using Summer.Infra.Identity.Results;
using Summer.Shared.Dtos;

namespace Summer.Infra.Identity.Services
{
    public interface IIdentityService
    {
        Task<OutputDto<AuthenticationResult>> Login(string username, string password);

        Task<OutputDto<AuthenticationResult>> Register(string username, string password);

        Task<OutputDto<AuthenticationResult>> RefreshToken(string token, string refreshToken);
    }
}