using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Summer.Core.Jwt
{
    public class JwtTokenHelper : IJwtTokenHelper
    {
        private readonly IOptions<SummerOptions> _summerOptions;

        public JwtTokenHelper(IOptions<SummerOptions> summerOptions)
        {
            _summerOptions = summerOptions;
        }

        public JwtToken CreateJwtToken(JwtUser jwtUser)
        {
            //创建用户身份标识，可按需要添加更多信息
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, jwtUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                new Claim (JwtRegisteredClaimNames.Exp,
                    $"{new DateTimeOffset(DateTime.Now.AddSeconds(_summerOptions.Value.JwtOptions.ExpireSeconds)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Expiration,
                    DateTime.Now.AddSeconds(_summerOptions.Value.JwtOptions.ExpireSeconds).ToString(CultureInfo.InvariantCulture)),
                new Claim(JwtRegisteredClaimNames.Iss,_summerOptions.Value.JwtOptions.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud,_summerOptions.Value.JwtOptions.Audience),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_summerOptions.Value.JwtOptions.SecurityKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //创建令牌
            var token = new JwtSecurityToken(
                signingCredentials: cred,
                claims: claims
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtToken() { Token = jwtToken }; ;
        }
    }
}
