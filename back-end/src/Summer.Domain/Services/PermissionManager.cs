using System.Threading.Tasks;
using Summer.Domain.Interfaces;

namespace Summer.Domain.Services
{
    public class PermissionManager : IPermissionManager
    {
        public Task<string> GetUserPermissionCodesAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> CheckUserPermissionCodeAsync(int userId, string permissionCode)
        {
            // var permissions = await GetUserPermissionCodesAsync(userId);
            //
            // return permissions.Contains(permissionCode);

            return await Task.FromResult(true);
        }
    }
}