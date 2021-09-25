using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;

namespace Summer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            var response = await _mediator.Send(loginCommand);
            return Ok(response);
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            var response = await _mediator.Send(registerCommand);
            return Ok(response);
        }
    }
}