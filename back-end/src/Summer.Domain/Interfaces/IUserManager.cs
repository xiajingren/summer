using System.Collections.Generic;
using System.Threading.Tasks;
using Summer.Domain.Entities;

namespace Summer.Domain.Interfaces
{
    public interface IUserManager
    {
        Task<User> CreateAsync(string userName, string password, IEnumerable<int> roleIds = null);

        Task UpdateAsync(User user, string userName, string password);

        Task<bool> CheckPasswordAsync(User user, string password);
    }
}