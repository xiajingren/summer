using Ardalis.GuardClauses;

namespace Summer.Infrastructure.MultiTenancy
{
    public class Tenant
    {
        public int Id { get; protected set; }

        public string TenantCode { get; private set; }

        public string TenantName { get; private set; }

        public string ConnectionString { get; private set; }
        
        public Tenant(string tenantCode, string tenantName, string connectionString = null)
        {
            TenantCode = Guard.Against.NullOrEmpty(tenantCode, nameof(tenantCode));
            TenantName = Guard.Against.NullOrEmpty(tenantName, nameof(tenantName));
            ConnectionString = connectionString;
        }
    }
}