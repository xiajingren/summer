using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
{
    [Permission(nameof(GetTenantsQuery), "获取租户列表", PermissionConstants.TenantGroupName)]
    public class GetTenantsQuery : PaginationQuery, IRequest<PaginationResponse<TenantResponse>>
    {
    }
}