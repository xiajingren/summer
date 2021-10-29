using Ardalis.GuardClauses;

namespace Summer.Infrastructure.MultiTenancy
{
    public class Tenant
    {
        public int Id { get; private set; }

        public string TenantCode { get; private set; }

        public string TenantName { get; private set; }

        public string ConnectionString { get; private set; }

        public Tenant(string tenantCode, string tenantName, string connectionString = null)
        {
            TenantCode = Guard.Against.NullOrEmpty(tenantCode, nameof(tenantCode));
            TenantName = Guard.Against.NullOrEmpty(tenantName, nameof(tenantName));
            ConnectionString = connectionString;
        }

        private Tenant(int id, string tenantCode, string tenantName, string connectionString = null)
        {
            Id = id;
            TenantCode = tenantCode;
            TenantName = tenantName;
            ConnectionString = connectionString;
        }

        public static Tenant Default => new(1, "Default", "默认租户");
    }
}