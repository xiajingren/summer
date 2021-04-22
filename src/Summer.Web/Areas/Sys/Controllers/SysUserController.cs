using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Base.IServices;
using Summer.App.Contracts.Business.Dtos;
using Summer.App.Contracts.Business.IServices;

namespace Summer.Web.Areas.Sys.Controllers
{
    [Area("Sys")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        private readonly ISysUserService _sysUserService;
        private readonly ICurrentUserService _currentUserService;

        public SysUserController(ISysUserService sysUserService, ICurrentUserService currentUserService)
        {
            _sysUserService = sysUserService;
            _currentUserService = currentUserService;
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
            return await _currentUserService.Get();
        }
    }
}