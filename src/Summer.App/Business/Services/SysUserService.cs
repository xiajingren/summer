using System;
using System.Linq;
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

        public override IQueryable<SysUser> GetQueryable(PagedInputDto pagedInputDto = null)
        {
            var query = base.GetQueryable(pagedInputDto);
            query = query.Include(p => p.Avatar);

            if (!string.IsNullOrEmpty(pagedInputDto?.Query))
            {
                query = query.Where(p => p.Name.Contains(pagedInputDto.Query) || p.Account.Contains(pagedInputDto.Query));
            }

            return query;
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