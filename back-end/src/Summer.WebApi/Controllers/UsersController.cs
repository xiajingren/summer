using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Summer.Application.Requests.Commands;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;

namespace Summer.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<UserResponse>>> GetUsers(
            [FromQuery] GetUsersQuery getUsersQuery)
        {
            var response = await _mediator.Send(getUsersQuery);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UserResponse>> CreateUser(CreateUserCommand createUserCommand)
        {
            var response = await _mediator.Send(createUserCommand);
            return CreatedAtAction(nameof(GetUser), new { id = response.Id }, response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserCommand updateUserCommand)
        {
            await _mediator.Send(updateUserCommand);
            return NoContent();
        }

        [HttpPut("{id:int}/change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUserPassword(int id, UpdateUserPasswordCommand updateUserPasswordCommand)
        {
            await _mediator.Send(updateUserPasswordCommand);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> RegisterUser([FromBody] RegisterUserCommand registerUserCommand)
        {
            var response = await _mediator.Send(registerUserCommand);
            return Ok(response);
        }
    }
}