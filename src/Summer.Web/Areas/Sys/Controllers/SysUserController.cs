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

        [HttpPost("[action]")]
        public async Task<BaseDto<object>> Hello(SysUserDto value)
        {
            await _sysUserService.Hello();
            return BaseDto<object>.CreateOkInstance("Hello!!!");
        }

    }
}
