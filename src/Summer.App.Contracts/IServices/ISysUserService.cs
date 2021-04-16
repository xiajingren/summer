using System.Threading.Tasks;
using Summer.App.Contracts.Dtos;

namespace Summer.App.Contracts.IServices
{
    public interface ISysUserService : IBaseCrudService<BasePagedReqDto, SysUserDto, SysUserDto, SysUserDto>
    {
        Task<BaseDto<SysUserDto>> Login(LoginDto value);
    }
}