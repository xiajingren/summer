using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Summer.Application.Requests.Commands;

namespace Summer.Application.MapperProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateRoleCommand, IdentityRole<int>>();
            CreateMap<UpdateRoleCommand, IdentityRole<int>>();
        }
    }
}