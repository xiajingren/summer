using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.Extensions;
using Summer.Domain.Interfaces;
using Summer.Domain.Options;
using Summer.Domain.Results;
using Summer.Infrastructure.Constants;
using Summer.Shared.Utils;

namespace Summer.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(UserManager<User> userManager,
            TokenValidationParameters tokenValidationParameters,
            IOptions<JwtOptions> jwtOptions,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<TokenResult> RegisterAsync(string username, string password)
        {
            if (username.Length < 5)
            {
                throw new BusinessException("用户名不合法");
            }

            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
            {
                throw new BusinessException("用户名已存在");
            }

            var newUser = new User() { UserName = username };
            var isCreated = await _userManager.CreateAsync(newUser, password);
            if (!isCreated.Succeeded)
            {
                throw new DetailErrorsBusinessException(isCreated.Errors.ToDetailErrors());
            }

            return await GenerateJwtToken(newUser);
        }

        public async Task<TokenResult> LoginAsync(string username, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser == null)
            {
                throw new BusinessException("用户名或密码错误");
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, password);
            if (!isCorrect)
            {
                throw new BusinessException("用户名或密码错误");
            }

            return await GenerateJwtToken(existingUser);
        }

        public async Task<TokenResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var claimsPrincipal =
                jwtTokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
            var validatedSecurityAlgorithm = validatedToken is JwtSecurityToken jwtSecurityToken
                                             && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                                 StringComparison.InvariantCultureIgnoreCase);
            if (!validatedSecurityAlgorithm)
            {
                throw new BusinessException("无效的token...");
            }

            var expiryDateUnix =
                long.Parse(claimsPrincipal.Claims.Single(x => x.Type == ClaimConstants.Expiry).Value);
            var expiryDateTimeUtc = CommonHelper.Instance.UnixTimeStampToDateTime(expiryDateUnix);
            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                throw new BusinessException("token未过期...");
            }

            var jti = claimsPrincipal.Claims.Single(x => x.Type == ClaimConstants.Jti).Value;

            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (storedRefreshToken == null)
            {
                throw new BusinessException("无效的refresh_token...");
            }

            storedRefreshToken.Confirm(jti);

            await _refreshTokenRepository.UpdateAsync(storedRefreshToken);

            var dbUser = await _userManager.FindByIdAsync(storedRefreshToken.UserId.ToString());
            return await GenerateJwtToken(dbUser);
        }

        private async Task<TokenResult> GenerateJwtToken(User user)
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

            return new TokenResult()
            {
                AccessToken = token,
                ExpiresIn = (int)_jwtOptions.ExpiresIn.TotalSeconds,
                RefreshToken = refreshToken.Token
            };
        }
    }
}