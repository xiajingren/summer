using System.Collections.Generic;
using System.Threading.Tasks;
using Summer.Domain.Entities;

namespace Summer.Domain.Interfaces
{
    public interface IPermissionManager
    {
        Task<IEnumerable<Permission>> GetPermissionsAsync(int targetId, PermissionType permissionType);

        Task<bool> CheckUserPermissionAsync(int userId, string permissionCode);
    }
}