using Summer.App.Contracts.Dtos;
using Summer.App.Contracts.IServices;
using Summer.App.Entities;
using System;

namespace Summer.App.Services
{
    internal class SysUserService : BaseCrudService<SysUser, BasePagedReqDto, SysUserDto, SysUserDto, SysUserDto>, ISysUserService
    {
        public SysUserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

    }
}