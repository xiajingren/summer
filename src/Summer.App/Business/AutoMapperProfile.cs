using AutoMapper;
using Summer.App.Business.Entities;
using Summer.App.Contracts.Business.Dtos;

namespace Summer.App.Business
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SysUserDto, SysUser>();
            CreateMap<SysUser, SysUserDto>();
        }
    }
}