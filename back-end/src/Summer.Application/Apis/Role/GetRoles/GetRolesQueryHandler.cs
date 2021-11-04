using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Apis.Role.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginationResponse<RoleResponse>>
    {
        private readonly IReadRepository<Domain.Entities.Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetRolesQueryHandler(IReadRepository<Domain.Entities.Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<RoleResponse>> Handle(GetRolesQuery request,
            CancellationToken cancellationToken)
        {
            var rowCount = await _roleRepository.CountAsync(new RoleSpec(request.Filter), cancellationToken);
            var roles = await _roleRepository.ListAsync(
                new RoleSpec(request.Filter, request.GetSkip(), request.PageSize),
                cancellationToken);

            return new PaginationResponse<RoleResponse>(request.PageIndex, request.PageSize, rowCount,
                _mapper.Map<IEnumerable<RoleResponse>>(roles));
        }
    }
}