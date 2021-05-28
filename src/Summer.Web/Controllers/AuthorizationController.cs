using Microsoft.AspNetCore.Mvc;
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
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ISysUserService _sysUserService;

        public AuthorizationController(IJwtTokenService jwtTokenService, ISysUserService sysUserService)
        {
            _jwtTokenService = jwtTokenService;
            _sysUserService = sysUserService;
        }

        [HttpPost("[action]")]
        public async Task<OutputDto<TokenDto>> Token(LoginDto value)
        {
            var result = await _sysUserService.Login(value);
            if (result.Code == 0)
            {
                return OutputDto<TokenDto>.CreateFailInstance(null, result.Message);
            }

            //todo:
            var token = _jwtTokenService.CreateJwtToken(result.Data);
            return OutputDto<TokenDto>.CreateOkInstance(token);
        }
    }
}