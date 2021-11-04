using AutoMapper;
using Summer.Application.Apis.Auth.GetCurrentUserProfile;
using Summer.Application.Apis.Permission;
using Summer.Application.Apis.Role;
using Summer.Application.Apis.Tenant;
using Summer.Application.Apis.User;
using Summer.Application.Interfaces;
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
                .ForMember(x => x.Id, opts => opts.MapFrom(r => r.RoleId))
                .ForMember(x => x.Name, opts => opts.Ignore());
            CreateMap<Permission, PermissionResponse>();
            CreateMap<Tenant, TenantResponse>();
        }
    }
}