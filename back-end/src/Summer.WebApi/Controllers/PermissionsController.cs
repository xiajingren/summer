using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;

namespace Summer.WebApi.Controllers
{
    [Route("api/permissions")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PermissionGroupInfoResponse>>> GetAllPermissions()
        {
            var response = await _mediator.Send(new GetPermissionGroupsQuery());
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionResponse>>> GetPermissions(
            [FromQuery] GetPermissionsQuery getPermissionsQuery)
        {
            var response = await _mediator.Send(getPermissionsQuery);
            return Ok(response);
        }
    }
}