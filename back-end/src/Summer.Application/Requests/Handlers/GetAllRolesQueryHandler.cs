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
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleResponse>>
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(IRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<RoleResponse>> Handle(GetAllRolesQuery request,
            CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.ListAsync(new RoleSpec(), cancellationToken);
            return _mapper.Map<IEnumerable<RoleResponse>>(roles);
        }
    }
}