using Microsoft.IdentityModel.Tokens;
using Summer.App.Contracts;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Base.IServices;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Summer.App.Contracts.Base.Consts;

namespace Summer.App.Base.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly AppOptions _appOptions;

        public JwtTokenService(AppOptions appOptions)
        {
            _appOptions = appOptions;
        }

        public TokenDto CreateJwtToken(CurrentUserDto currentUserDto)
        {
            //创建用户身份标识，可按需要添加更多信息
            var claims = new Claim[]
            {
                new Claim(AppClaimTypes.Id, currentUserDto.Id.ToString()),
                new Claim(AppClaimTypes.Account, currentUserDto.Account),
                
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                new Claim (JwtRegisteredClaimNames.Exp,
                    $"{new DateTimeOffset(DateTime.Now.AddSeconds(_appOptions.JwtOptions.ExpireSeconds)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Expiration,
                    DateTime.Now.AddSeconds(_appOptions.JwtOptions.ExpireSeconds).ToString(CultureInfo.InvariantCulture)),
                new Claim(JwtRegisteredClaimNames.Iss,_appOptions.JwtOptions.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud,_appOptions.JwtOptions.Audience),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appOptions.JwtOptions.SecurityKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //创建令牌
            var token = new JwtSecurityToken(
                signingCredentials: cred,
                claims: claims
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenDto() { Token = jwtToken }; ;
        }
    }
}
