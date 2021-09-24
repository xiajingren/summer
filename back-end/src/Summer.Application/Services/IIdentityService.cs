using System.Threading.Tasks;
using Summer.Application.Requests;
using Summer.Application.Responses;
using Summer.Shared.Dtos;

namespace Summer.Application.Services
{
    public interface IIdentityService
    {
        Task<OutputDto<TokenResponse>> Login(string username, string password);

        Task<OutputDto<TokenResponse>> Register(RegisterRequest input);

        Task<OutputDto<TokenResponse>> RefreshToken(string token, string refreshToken);
    }
}