using System.Threading.Tasks;

namespace Summer.Domain.Interfaces
{
    public interface IPermissionManager
    {
        Task<string> GetUserPermissionCodesAsync(int userId);

        Task<bool> CheckUserPermissionCodeAsync(int userId, string permissionCode);
    }
}