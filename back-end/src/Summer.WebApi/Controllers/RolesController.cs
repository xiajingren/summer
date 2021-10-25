using System.Collections.Generic;
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
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAllRoles()
        {
            var response = await _mediator.Send(new GetAllRolesQuery());
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<RoleResponse>>> GetRoles(
            [FromQuery] GetRolesQuery getRolesQuery)
        {
            var response = await _mediator.Send(getRolesQuery);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleResponse>> GetRole(int id)
        {
            var response = await _mediator.Send(new GetRoleByIdQuery(id));
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<RoleResponse>> CreateRole(CreateRoleCommand createRoleCommand)
        {
            var response = await _mediator.Send(createRoleCommand);
            return CreatedAtAction(nameof(GetRole), new {id = response.Id}, response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateRole(int id, UpdateRoleCommand updateRoleCommand)
        {
            await _mediator.Send(updateRoleCommand);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _mediator.Send(new DeleteRoleCommand(id));
            return NoContent();
        }
    }
}