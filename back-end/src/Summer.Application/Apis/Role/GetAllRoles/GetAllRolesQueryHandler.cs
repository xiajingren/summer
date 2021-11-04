using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Apis.Role.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleResponse>>
    {
        private readonly IReadRepository<Domain.Entities.Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(IReadRepository<Domain.Entities.Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleResponse>> Handle(GetAllRolesQuery request,
            CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.ListAsync(new RoleSpec(), cancellationToken);
            return _mapper.Map<IEnumerable<RoleResponse>>(roles);
        }
    }
}