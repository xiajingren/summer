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
        
        public SysUserController(ISysUserService sysUserService, ICurrentUserService currentUserService)
        {
            _sysUserService = sysUserService;
        }

        [HttpGet]
        public async Task<OutputDto<PagedOutputDto<SysUserDto>>> Get([FromQuery] PagedInputDto value)
        {
            return await _sysUserService.Get(value);
        }

        [HttpGet("{id}")]
        public async Task<OutputDto<SysUserDto>> Get(Guid id)
        {
            return await _sysUserService.Get(id);
        }

        [HttpPost]
        public async Task<OutputDto<SysUserDto>> Post(SysUserDto value)
        {
            return await _sysUserService.Create(value);
        }

        [HttpPut("{id}")]
        public async Task<OutputDto<SysUserDto>> Put(Guid id, SysUserDto value)
        {
            return await _sysUserService.Update(id, value);
        }

        [HttpDelete("{id}")]
        public async Task<OutputDto<SysUserDto>> Delete(Guid id)
        {
            return await _sysUserService.Delete(id);
        }

    }
}