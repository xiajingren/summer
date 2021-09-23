using System.Threading.Tasks;
using Summer.Infra.Identity.Dtos;
using Summer.Shared.Dtos;

namespace Summer.Infra.Identity.Services
{
    public interface IIdentityService
    {
        Task<OutputDto<TokenOutputDto>> Login(string username, string password);

        Task<OutputDto<TokenOutputDto>> Register(RegisterInputDto input);

        Task<OutputDto<TokenOutputDto>> RefreshToken(string token, string refreshToken);
    }
}