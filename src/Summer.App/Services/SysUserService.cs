using Summer.App.Contracts.Dtos;
using Summer.App.Contracts.IServices;
using Summer.App.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Summer.App.Services
{
    internal class SysUserService : BaseCrudService<SysUser, SysUserDto>, ISysUserService
    {
        public SysUserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<BaseDto<SysUserDto>> Login(LoginDto value)
        {
            var model = await SummerDbContext.SysUsers
                .SingleOrDefaultAsync(p => p.UserName == value.UserName && p.Password == value.Password);

            if (model == null)
            {
                return BaseDto<SysUserDto>.CreateFailInstance(null, "登录失败，用户名或密码错误");
            }

            return BaseDto<SysUserDto>.CreateOkInstance(Mapper.Map<SysUserDto>(model));
        }

    }
}