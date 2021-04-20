using Summer.App.Contracts.Base.Dtos;

namespace Summer.App.Contracts.Base.IServices
{
    public interface IJwtTokenService
    {
        TokenDto CreateJwtToken(CurrentUserDto jwtUser);
    }
}