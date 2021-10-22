namespace Summer.Domain.Interfaces
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }

        string Id { get; }

        string UserName { get; }
    }
}