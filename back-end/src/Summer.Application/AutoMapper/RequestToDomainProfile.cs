using AutoMapper;
using Summer.Application.Requests;
using Summer.Domain.Identity.Commands;

namespace Summer.Application.AutoMapper
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<RegisterRequest, RegisterCommand>();
        }
    }
}