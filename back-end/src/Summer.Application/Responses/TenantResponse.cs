namespace Summer.Application.Responses
{
    public class TenantResponse
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public string Host { get; set; }

        public TenantResponse(int id, string code, string name, string connectionString, string host)
        {
            Id = id;
            Code = code;
            Name = name;
            ConnectionString = connectionString;
            Host = host;
        }
    }
}