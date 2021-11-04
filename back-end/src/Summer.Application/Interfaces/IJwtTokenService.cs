using System.Threading.Tasks;
using Summer.Application.Apis.Auth;
using Summer.Domain.Entities;

namespace Summer.Application.Interfaces
{
    public interface IJwtTokenService
    {
        bool ValidateExpiredToken(string token, out string jwtId);

        Task<TokenResponse> IssueTokenAsync(User user);
    }
}