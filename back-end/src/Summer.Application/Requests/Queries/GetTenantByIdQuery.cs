using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
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