namespace Summer.Core.Jwt
{
    public interface IJwtTokenHelper
    {
        JwtToken CreateJwtToken(JwtUser jwtUser);
    }
}