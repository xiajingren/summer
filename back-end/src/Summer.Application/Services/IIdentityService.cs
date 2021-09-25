using System.Threading.Tasks;
using Summer.Application.Requests;
using Summer.Application.Responses;
using Summer.Shared.Dtos;

namespace Summer.Application.Services
{
    public interface IIdentityService
    {
        Task<OutputDto<TokenResponse>> LoginAsync(LoginRequest loginRequest);

        Task<TokenResponse> RegisterAsync(RegisterRequest registerRequest);

        Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    }
}