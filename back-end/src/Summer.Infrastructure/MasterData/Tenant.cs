using Ardalis.GuardClauses;
using Summer.Application.Responses;

namespace Summer.Infrastructure.MasterData
{
    public class Tenant
    {
        public int Id { get; private set; }

        public string Code { get; private set; }

        public string Name { get; private set; }

        public string ConnectionString { get; private set; }

        public string Host { get; private set; }

        private Tenant()
        {
        }

        public Tenant(string code, string name, string host = null, string connectionString = null)
        {
            Code = Guard.Against.NullOrEmpty(code, nameof(code));
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
            Host = host;
            ConnectionString = connectionString;
        }

        private Tenant(int id, string tenantCode, string tenantName)
        {
            Id = id;
            Code = tenantCode;
            Name = tenantName;
        }

        public static Tenant Default => new(1, "Default", "默认租户");

        public TenantResponse ToResponse()
        {
            return new(Id, Code, Name, ConnectionString, Host);
        }
    }
}