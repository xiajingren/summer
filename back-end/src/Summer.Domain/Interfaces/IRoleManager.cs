using System.Threading.Tasks;
using Summer.Domain.Entities;

namespace Summer.Domain.Interfaces
{
    public interface IRoleManager
    {
        Task<Role> CreateAsync(string name);

        Task UpdateAsync(Role role, string name);
    }
}