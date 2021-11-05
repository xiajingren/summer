using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Tenants.UpdateTenant
{
    [Permission(nameof(UpdateTenantCommand), "修改租户", PermissionConstants.TenantGroupName)]
    public class UpdateTenantCommand : IRequest
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public string Host { get; set; }

        public UpdateTenantCommand(int id, string code, string name, string connectionString, string host)
        {
            Id = id;
            Code = code;
            Name = name;
            ConnectionString = connectionString;
            Host = host;
        }
    }
}