namespace Summer.Application.Interfaces
{
    public interface ICurrentTenant
    {
        int Id { get; }
        string Code { get; }
        string Name { get; }
        string ConnectionString { get; }
        string Host { get; }
    }
}