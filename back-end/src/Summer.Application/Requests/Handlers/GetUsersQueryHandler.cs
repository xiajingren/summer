using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginationResponse<UserResponse>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IRepository<User> userRepository, IRepository<Role> roleRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PaginationResponse<UserResponse>> Handle(GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            var rowCount = await _userRepository.CountAsync(new UserPaginatedSpec(request.Filter), cancellationToken);
            var users = await _userRepository.ListAsync(
                new UserPaginatedSpec(request.Filter, request.GetSkip(), request.PageSize),
                cancellationToken);

            var roles = await _roleRepository.ListAsync(cancellationToken);

            var responseUsers = _mapper.Map<IEnumerable<UserResponse>>(users);
            foreach (var responseUser in responseUsers)
            {
                responseUser.Roles =
                    _mapper.Map<IEnumerable<RoleResponse>>(roles.Where(x =>
                        responseUser.Roles.Select(r => r.Id).Contains(x.Id)));
            }

            return new PaginationResponse<UserResponse>(request.PageIndex, request.PageSize, rowCount,
                responseUsers);
        }
    }
}