using System.Collections.Generic;
using System.Threading.Tasks;

namespace Summer.Domain.Interfaces
{
    public interface IPermissionManager
    {
        Task<IEnumerable<string>> GetCodesByUserIdAsync(int id);
    }
}