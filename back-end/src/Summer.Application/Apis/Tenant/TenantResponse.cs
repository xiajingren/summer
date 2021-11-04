namespace Summer.Application.Apis.Tenant
{
    public class TenantResponse
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public string Host { get; set; }
    }
}