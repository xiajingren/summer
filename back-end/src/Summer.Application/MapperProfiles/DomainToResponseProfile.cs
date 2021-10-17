using AutoMapper;
using Summer.Application.Responses;
using Summer.Infrastructure.Identity.Dtos;

namespace Summer.Application.MapperProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<TokenOutputDto, TokenResponse>();
        }
    }
}