using System.Threading.Tasks;
using Summer.Domain.Entities;

namespace Summer.Application.Interfaces
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }

        int Id { get; }

        string UserName { get; }

        Task<User> GetUserAsync();
    }
}