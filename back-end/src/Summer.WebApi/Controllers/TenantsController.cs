using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Summer.Application.Apis;
using Summer.Application.Apis.Tenants;
using Summer.Application.Apis.Tenants.CreateTenant;
using Summer.Application.Apis.Tenants.DeleteTenant;
using Summer.Application.Apis.Tenants.GetTenantById;
using Summer.Application.Apis.Tenants.GetTenants;
using Summer.Application.Apis.Tenants.UpdateTenant;

namespace Summer.WebApi.Controllers
{
    [Route("api/tenants")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<TenantResponse>>> GetTenants(
            [FromQuery] GetTenantsQuery getTenantsQuery)
        {
            var response = await _mediator.Send(getTenantsQuery);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TenantResponse>> GetTenant(int id)
        {
            var response = await _mediator.Send(new GetTenantByIdQuery(id));
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TenantResponse>> CreateTenant(CreateTenantCommand createTenantCommand)
        {
            var response = await _mediator.Send(createTenantCommand);
            return CreatedAtAction(nameof(GetTenant), new { id = response.Id }, response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTenant(int id, UpdateTenantCommand updateTenantCommand)
        {
            await _mediator.Send(updateTenantCommand);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            await _mediator.Send(new DeleteTenantCommand(id));
            return NoContent();
        }
    }
}