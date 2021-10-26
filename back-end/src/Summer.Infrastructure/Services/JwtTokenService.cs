using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Summer.Application.Constants;
using Summer.Application.Interfaces;
using Summer.Application.Responses;
using Summer.Domain.Entities;
using Summer.Domain.SeedWork;
using Summer.Infrastructure.Constants;
using Summer.Infrastructure.Options;
using Summer.Shared.Utils;

namespace Summer.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public JwtTokenService(IOptions<JwtOptions> jwtOptions, TokenValidationParameters tokenValidationParameters,
            IRepository<RefreshToken> refreshTokenRepository)
        {
            _jwtOptions = jwtOptions.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public string GenerateToken(User user, out string jwtId)
        {
            var key = Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimConstants.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(ClaimConstants.UserId, user.Id.ToString()),
                    new Claim(ClaimConstants.UserName, user.UserName)
                }),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.Add(_jwtOptions.ExpiresIn),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtTokenHandler.CreateToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(securityToken);

            jwtId = securityToken.Id;
            return token;
        }

        public bool ValidateExpiredToken(string token, out string jwtId)
        {
            jwtId = default;
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var claimsPrincipal =
                jwtTokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
            var validatedSecurityAlgorithm = validatedToken is JwtSecurityToken jwtSecurityToken
                                             && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                                 StringComparison.InvariantCultureIgnoreCase);
            if (!validatedSecurityAlgorithm)
            {
                return false;
            }

            var expiryDateUnix =
                long.Parse(claimsPrincipal.Claims.Single(x => x.Type == ClaimConstants.Expiry).Value);
            var expiryDateTimeUtc = CommonHelper.UnixTimeStampToDateTime(expiryDateUnix);
            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return false;
            }

            jwtId = claimsPrincipal.Claims.Single(x => x.Type == ClaimConstants.Jti).Value;
            return true;
        }

        public async Task<TokenResponse> IssueTokenAsync(User user)
        {
            var key = Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimConstants.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(ClaimConstants.UserId, user.Id.ToString()),
                    new Claim(ClaimConstants.UserName, user.UserName)
                }),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.Add(_jwtOptions.ExpiresIn),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtTokenHandler.CreateToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(securityToken);

            var refreshToken = new RefreshToken(securityToken.Id, user.Id);

            await _refreshTokenRepository.AddAsync(refreshToken);

            return new TokenResponse(token, (int) _jwtOptions.ExpiresIn.TotalSeconds, "Bearer", refreshToken.Token);
        }
    }
}