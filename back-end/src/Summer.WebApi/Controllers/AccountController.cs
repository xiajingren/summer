using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Summer.Application.Requests;
using Summer.Application.Services;

namespace Summer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var response = await _identityService.RegisterAsync(registerRequest);
            return Ok(response);
        }
    }
}