namespace Summer.Application.Interfaces
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }

        int Id { get; }

        string UserName { get; }
    }
}