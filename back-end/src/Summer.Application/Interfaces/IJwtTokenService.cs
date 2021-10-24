using System.Threading.Tasks;
using Summer.Application.Responses;
using Summer.Domain.Entities;

namespace Summer.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user, out string jwtId);

        bool ValidateExpiredToken(string token, out string jwtId);

        Task<TokenResponse> IssueTokenAsync(User user);
    }
}