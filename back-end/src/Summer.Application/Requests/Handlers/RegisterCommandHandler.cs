using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Summer.Application.Options;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Shared.Utils;

namespace Summer.Application.Requests.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, TokenResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<TokenResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new DomainException("用户名已存在");
            }

            var newUser = new ApplicationUser() {UserName = request.UserName};
            var isCreated = await _userManager.CreateAsync(newUser, request.Password);
            if (!isCreated.Succeeded)
            {
                throw new DomainException(string.Join(";", isCreated.Errors.Select(p => p.Description)));
            }

            return GenerateJwtToken(newUser);
        }

        private TokenResponse GenerateJwtToken(ApplicationUser user)
        {
            var key = Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id)
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

            //var refreshToken = new RefreshToken()
            //{
            //    JwtId = token.Id,
            //    IsUsed = false,
            //    IsRevorked = false,
            //    UserId = user.Id,
            //    AddedDate = DateTime.UtcNow,
            //    ExpiryDate = DateTime.UtcNow.AddMonths(6),
            //    Token = RandomString(35) + Guid.NewGuid()
            //};

            //await _apiDbContext.RefreshTokens.AddAsync(refreshToken);
            //await _apiDbContext.SaveChangesAsync();

            //return new AuthResult()
            //{
            //    Token = jwtToken,
            //    Success = true,
            //    RefreshToken = refreshToken.Token
            //};

            return new TokenResponse()
            {
                AccessToken = token,
                ExpiresIn = (int) _jwtOptions.ExpiresIn.TotalSeconds,
                RefreshToken = CommonHelper.Instance.GenerateRandomNumber()
            };
        }
    }
}