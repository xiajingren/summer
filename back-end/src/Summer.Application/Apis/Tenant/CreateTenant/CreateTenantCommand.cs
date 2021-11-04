using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Tenant.CreateTenant
{
    [Permission(nameof(CreateTenantCommand), "创建租户", PermissionConstants.TenantGroupName)]
    public class CreateTenantCommand : IRequest<TenantResponse>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public string Host { get; set; }

        public CreateTenantCommand(string code, string name, string connectionString, string host)
        {
            Code = code;
            Name = name;
            ConnectionString = connectionString;
            Host = host;
        }
    }
}