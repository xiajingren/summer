using System.Threading.Tasks;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Business.Dtos;

namespace Summer.App.Contracts.Base.IServices
{
    /// <summary>
    /// 当前登录用户
    /// </summary>
    public interface ICurrentUserService
    {
        Task<BaseDto<SysUserDto>> Get();
    }
}