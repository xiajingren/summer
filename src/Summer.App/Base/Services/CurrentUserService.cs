using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Summer.App.Business.Entities;
using Summer.App.Contracts.Base.Consts;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Base.IServices;
using Summer.App.Contracts.Business.Dtos;

namespace Summer.App.Base.Services
{
    internal class CurrentUserService : BaseService, ICurrentUserService
    {
        private ClaimsPrincipal ClaimsPrincipal { get; }

        public CurrentUserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            ClaimsPrincipal = httpContextAccessor.HttpContext?.User;
        }

        internal async Task<SysUser> GetEntity()
        {
            var id = ClaimsPrincipal?.FindFirst(AppClaimTypes.Id)?.Value;
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var userId = Guid.Parse(id);
            return await AppDbContext.SysUsers.SingleOrDefaultAsync(p => p.Id == userId);
        }

        public async Task<BaseDto<SysUserDto>> Get()
        {
            var user = await GetEntity();
            return user == null
                ? Fail<SysUserDto>(null)
                : Ok(Mapper.Map<SysUserDto>(user));
        }
    }
}