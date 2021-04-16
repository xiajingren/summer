using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Summer.App.Contracts.Dtos;
using Summer.App.Contracts.IServices;
using System.Threading.Tasks;

namespace Summer.Web.Areas.Sys.Controllers
{
    [Area("Sys")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        private readonly ISysUserService _sysUserService;

        public SysUserController(ISysUserService sysUserService)
        {
            _sysUserService = sysUserService;
        }

        [HttpGet]
        public async Task<BaseDto<BasePagedDto<SysUserDto>>> Get([FromQuery] BasePagedReqDto value)
        {
            return await _sysUserService.Get(value);
        }

        [HttpGet("{id}")]
        public async Task<BaseDto<SysUserDto>> Get(Guid id)
        {
            return await _sysUserService.Get(id);
        }

        [HttpPost]
        public async Task<BaseDto<SysUserDto>> Post([FromBody] SysUserDto value)
        {
            return await _sysUserService.Create(value);
        }

        [HttpPut("{id}")]
        public async Task<BaseDto<SysUserDto>> Put(Guid id, [FromBody] SysUserDto value)
        {
            return await _sysUserService.Update(id, value);
        }

        [HttpDelete("{id}")]
        public async Task<BaseDto<SysUserDto>> Delete(Guid id)
        {
            return await _sysUserService.Delete(id);
        }

        [HttpGet("[action]")]
        public async Task<BaseDto<SysUserDto>> Mine()
        {
            //todo:当前登录用户 服务
            var id = User?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            if (id == null) return null;

            var userId = Guid.Parse(id);

            return await _sysUserService.Get(userId);
        }

    }
}
