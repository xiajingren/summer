using Summer.Domain.Entities;

namespace Summer.Domain.Interfaces
{
    public interface IPasswordHashService
    {
        string Hash(User user, string password);

        bool Verify(User user, string password);
    }
}