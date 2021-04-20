using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Base.IServices;
using Summer.App.Contracts.Business.Dtos;
using Summer.App.Contracts.Business.IServices;

namespace Summer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenHelper;
        private readonly ISysUserService _sysUserService;

        public AuthorizationController(IJwtTokenService jwtTokenHelper, ISysUserService sysUserService)
        {
            _jwtTokenHelper = jwtTokenHelper;
            _sysUserService = sysUserService;
        }

        [HttpPost("[action]")]
        public async Task<BaseDto<TokenDto>> Token(LoginDto value)
        {
            var result = await _sysUserService.Login(value);
            if (result.Code == 0)
            {
                return BaseDto<TokenDto>.CreateFailInstance(null, result.Message);
            }

            //todo:
            var token = _jwtTokenHelper.CreateJwtToken(new CurrentUserDto() { Id = result.Data.Id.Value, });
            return BaseDto<TokenDto>.CreateOkInstance(token);
        }

    }
}
