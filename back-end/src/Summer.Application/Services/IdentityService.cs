using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Summer.Application.Options;
using Summer.Application.Requests;
using Summer.Application.Responses;
using Summer.Domain.Identity.Commands;
using Summer.Domain.Identity.Entities;
using Summer.Shared.Dtos;

namespace Summer.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly JwtOptions _jwtOptions;


        public IdentityService(IMediator mediator, IMapper mapper, IOptions<JwtOptions> jwtOptions)
        {
            _mediator = mediator;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
        }

        public Task<OutputDto<TokenResponse>> LoginAsync(LoginRequest loginRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TokenResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            var registerCommand = _mapper.Map<RegisterCommand>(registerRequest);
            var applicationUser = await _mediator.Send(registerCommand);
            return new TokenResponse();
        }

        public Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            throw new System.NotImplementedException();
        }

        private TokenResponse GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

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

            return new TokenResponse();
        }

    }
}