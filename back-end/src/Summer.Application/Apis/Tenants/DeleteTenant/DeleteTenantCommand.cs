using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Tenants.DeleteTenant
{
    [Permission(nameof(DeleteTenantCommand), "删除租户", PermissionConstants.TenantGroupName)]
    public class DeleteTenantCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteTenantCommand(int id)
        {
            Id = id;
        }
    }
}