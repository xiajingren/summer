using System.Collections.Generic;
using System.Threading.Tasks;
using Summer.Domain.Interfaces;

namespace Summer.Domain.Services
{
    public class PermissionManager : IPermissionManager
    {
        public async Task<IEnumerable<string>> GetCodesByUserIdAsync(int id)
        {
            return await Task.FromResult(new List<string> { "CreateRoleCommand" });
        }
    }
}