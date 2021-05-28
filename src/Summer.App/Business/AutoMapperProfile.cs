using AutoMapper;
using Summer.App.Business.Entities;
using Summer.App.Contracts.Business.Dtos;

namespace Summer.App.Business
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SysUser, SysUserDto>();
            CreateMap<SysUserDto, SysUser>();

            CreateMap<UploadFile, UploadFileDto>();
            CreateMap<UploadFileDto, UploadFile>();

            CreateMap<UploadFile, UploadFileOutputDto>();
            CreateMap<UploadFileOutputDto, UploadFile>();
        }
    }
}