using AutoMapper;
using Summer.Application.Responses;
using Summer.Infrastructure.Identity.Dtos;

namespace Summer.Application.AutoMapper
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<TokenOutput, TokenResponse>();
        }
    }
}