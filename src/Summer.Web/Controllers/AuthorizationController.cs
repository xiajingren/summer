using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Summer.App.Contracts.Dtos;
using Summer.Core.Jwt;

namespace Summer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public AuthorizationController(IJwtTokenHelper jwtTokenHelper)
        {
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpPost]
        public Task<BaseDto<JwtToken>> Token(SysUserDto value)
        {
            var token = _jwtTokenHelper.CreateJwtToken(new JwtUser() { Id = Guid.NewGuid() });
            return Task.FromResult(BaseDto<JwtToken>.CreateOkInstance(token));
        }
    }
}
