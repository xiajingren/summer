using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Tenant.GetTenants
{
    [Permission(nameof(GetTenantsQuery), "获取租户列表", PermissionConstants.TenantGroupName)]
    public class GetTenantsQuery : PaginationQuery, IRequest<PaginationResponse<TenantResponse>>
    {
    }
}