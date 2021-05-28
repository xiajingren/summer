using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Summer.App.Base.Services;
using Summer.App.Business.Entities;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Business.Dtos;
using Summer.App.Contracts.Business.IServices;

namespace Summer.App.Business.Services
{
    internal class SysUserService : BaseCrudService<SysUser, SysUserDto>, ISysUserService
    {
        public SysUserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<OutputDto<SysUserDto>> Login(LoginDto value)
        {
            var model = await AppDbContext.SysUsers
                .SingleOrDefaultAsync(p => p.Account == value.Account && p.Password == value.Password);

            return model == null
                ? Fail<SysUserDto>(null, "登录失败，用户名或密码错误")
                : Ok(Mapper.Map<SysUserDto>(model));
        }
    }
}