using Ardalis.GuardClauses;
using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class Tenant : BaseEntity, IAggregateRoot
    {
        public string Code { get; internal set; }

        public string Name { get; internal set; }

        public string ConnectionString { get; internal set; }

        public string Host { get; internal set; }

        private Tenant()
        {
            // required by EF
        }

        public Tenant(string code, string name, string host = null, string connectionString = null)
        {
            Code = Guard.Against.NullOrEmpty(code, nameof(code));
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
            Host = host;
            ConnectionString = connectionString;
        }

        private Tenant(int id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public static Tenant Default => new(1, "Default", "默认租户");
    }
}