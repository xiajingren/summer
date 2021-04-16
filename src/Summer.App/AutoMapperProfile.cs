using AutoMapper;
using Summer.App.Contracts.Dtos;
using Summer.App.Entities;

namespace Summer.App
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