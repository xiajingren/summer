using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Summer.Infrastructure.Identity.Dtos;
using Summer.Infrastructure.Identity.Entities;
using Summer.Infrastructure.Identity.Options;
using Summer.Shared.Exceptions;
using Summer.Shared.Utils;

namespace Summer.Infrastructure.Identity.Managers
{
    public class IdentityManager : IIdentityManager
    {
        private readonly UserManager<User> _userManager;
        private readonly UserDbContext _userDbContext;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtOptions _jwtOptions;

        public IdentityManager(UserManager<User> userManager, UserDbContext userDbContext,
            TokenValidationParameters tokenValidationParameters, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _userDbContext = userDbContext;
            _tokenValidationParameters = tokenValidationParameters;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<TokenOutput> RegisterAsync(string username, string password)
        {
            if (username.Length < 5)
            {
                throw new FriendlyException("用户名不合法");
            }

            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
            {
                throw new FriendlyException("用户名已存在");
            }

            var newUser = new User() {UserName = username};
            var isCreated = await _userManager.CreateAsync(newUser, password);
            if (!isCreated.Succeeded)
            {
                throw new FriendlyException("注册失败，用户名或密码不合法");
            }

            return await GenerateJwtToken(newUser);
        }

        public async Task<TokenOutput> LoginAsync(string username, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser == null)
            {
                throw new FriendlyException("用户名或密码错误");
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, password);
            if (!isCorrect)
            {
                throw new FriendlyException("用户名或密码错误");
            }

            return await GenerateJwtToken(existingUser);
        }

        public async Task<TokenOutput> RefreshTokenAsync(string token, string refreshToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var claimsPrincipal =
                jwtTokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
            var validatedSecurityAlgorithm = validatedToken is JwtSecurityToken jwtSecurityToken
                                             && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                                 StringComparison.InvariantCultureIgnoreCase);
            if (!validatedSecurityAlgorithm)
            {
                throw new FriendlyException("无效的token...");
            }

            var expiryDateUnix =
                long.Parse(claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = CommonHelper.Instance.UnixTimeStampToDateTime(expiryDateUnix);
            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                throw new FriendlyException("token未过期...");
            }

            var jti = claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken =
                await _userDbContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);
            if (storedRefreshToken == null)
            {
                throw new FriendlyException("无效的refresh_token...");
            }

            if (storedRefreshToken.ExpiryTime < DateTime.UtcNow)
            {
                throw new FriendlyException("refresh_token已过期...");
            }

            if (storedRefreshToken.Invalidated)
            {
                throw new FriendlyException("refresh_token已失效...");
            }

            if (storedRefreshToken.Used)
            {
                throw new FriendlyException("refresh_token已使用...");
            }

            if (storedRefreshToken.JwtId != jti)
            {
                throw new FriendlyException("refresh_token与此token不匹配...");
            }

            storedRefreshToken.Used = true;
            //_userDbContext.RefreshTokens.Update(storedRefreshToken);
            await _userDbContext.SaveChangesAsync();

            var dbUser = await _userManager.FindByIdAsync(storedRefreshToken.UserId.ToString());
            return await GenerateJwtToken(dbUser);
        }

        private async Task<TokenOutput> GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
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

            var refreshToken = new RefreshToken()
            {
                JwtId = securityToken.Id,
                UserId = user.Id,
                CreationTime = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddMonths(6),
                Token = CommonHelper.Instance.GenerateRandomNumber()
            };

            await _userDbContext.RefreshTokens.AddAsync(refreshToken);
            await _userDbContext.SaveChangesAsync();

            return new TokenOutput()
            {
                AccessToken = token,
                ExpiresIn = (int) _jwtOptions.ExpiresIn.TotalSeconds,
                RefreshToken = refreshToken.Token
            };
        }
    }
}