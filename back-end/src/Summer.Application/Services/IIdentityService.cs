using System.Threading.Tasks;
using Summer.Application.Dtos;
using Summer.Shared.Dtos;

namespace Summer.Application.Services
{
    public interface IIdentityService
    {
        Task<OutputDto<TokenOutputDto>> Login(string username, string password);

        Task<OutputDto<TokenOutputDto>> Register(RegisterInputDto input);

        Task<OutputDto<TokenOutputDto>> RefreshToken(string token, string refreshToken);
    }
}