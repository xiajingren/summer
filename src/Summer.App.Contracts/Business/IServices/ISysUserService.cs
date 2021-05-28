using System.Threading.Tasks;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Base.IServices;
using Summer.App.Contracts.Business.Dtos;

namespace Summer.App.Contracts.Business.IServices
{
    public interface ISysUserService : IBaseCrudService<SysUserDto>
    {
        Task<OutputDto<SysUserDto>> Login(LoginDto value);
    }
}