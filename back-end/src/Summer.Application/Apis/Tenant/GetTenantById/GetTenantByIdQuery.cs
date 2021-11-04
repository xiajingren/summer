using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Tenant.GetTenantById
{
    [Permission(nameof(GetTenantByIdQuery), "根据Id获取租户", PermissionConstants.TenantGroupName)]
    public class GetTenantByIdQuery : IRequest<TenantResponse>
    {
        public int Id { get; set; }

        public GetTenantByIdQuery(int id)
        {
            Id = id;
        }
    }
}