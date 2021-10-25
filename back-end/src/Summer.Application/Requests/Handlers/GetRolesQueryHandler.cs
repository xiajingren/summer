using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;
using Summer.Domain.Entities;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Requests.Handlers
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginationResponse<RoleResponse>>
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetRolesQueryHandler(IRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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