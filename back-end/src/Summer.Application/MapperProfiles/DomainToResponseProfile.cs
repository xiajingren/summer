﻿using AutoMapper;
using Summer.Application.Responses;
using Summer.Domain.Interfaces;
using Summer.Domain.Results;

namespace Summer.Application.MapperProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<TokenResult, TokenResponse>();
            CreateMap<ICurrentUser, UserProfileResponse>();
        }
    }
}