using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Business.Dtos;

namespace Summer.App.Contracts.Base.IServices
{
    public interface IJwtTokenService
    {
        TokenDto CreateJwtToken(SysUserDto userDto);
    }
}