﻿using AutoMapper;
using Summer.Application.Interfaces;
using Summer.Application.Responses;
using Summer.Domain.Entities;

namespace Summer.Application.MapperProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<ICurrentUser, CurrentUserProfileResponse>();
            CreateMap<Role, RoleResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<UserRole, RoleResponse>()
                .ForMember(x => x.Id, op => op.MapFrom(r => r.RoleId))
                .ForMember(x => x.Name, op => op.Ignore());
        }
    }
}