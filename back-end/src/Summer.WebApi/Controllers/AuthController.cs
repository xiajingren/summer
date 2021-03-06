using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Summer.Application.Apis.Auth;
using Summer.Application.Apis.Auth.GetCurrentUserProfile;
using Summer.Application.Apis.Auth.Login;
using Summer.Application.Apis.Auth.Refresh;
using Summer.Application.Apis.Auth.UpdateCurrentUserPassword;
using Summer.Application.Apis.Auth.UpdateCurrentUserProfile;

namespace Summer.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginCommand loginCommand)
        {
            var response = await _mediator.Send(loginCommand);
            return Ok(response);
        }
        
        [HttpPost("refresh")]
        public async Task<ActionResult<TokenResponse>> Refresh([FromBody] RefreshTokenCommand refreshTokenCommand)
        {
            var response = await _mediator.Send(refreshTokenCommand);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("my-profile")]
        public async Task<ActionResult<CurrentUserProfileResponse>> GetCurrentUserProfile()
        {
            var response = await _mediator.Send(new GetCurrentUserProfileQuery());
            return Ok(response);
        }

        [Authorize]
        [HttpPut("my-profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCurrentUserProfile(
            UpdateCurrentUserProfileCommand updateCurrentUserProfileCommand)
        {
            await _mediator.Send(updateCurrentUserProfileCommand);
            return NoContent();
        }

        [Authorize]
        [HttpPut("my-profile/change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCurrentUserPassword(
            UpdateCurrentUserPasswordCommand updateCurrentUserPasswordCommand)
        {
            await _mediator.Send(updateCurrentUserPasswordCommand);
            return NoContent();
        }
    }
}