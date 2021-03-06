using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Apis.Roles;
using Summer.Domain.Entities;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Apis.Users.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginationResponse<UserResponse>>
    {
        private readonly IReadRepository<User> _userRepository;
        private readonly IReadRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IReadRepository<User> userRepository, IReadRepository<Role> roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<UserResponse>> Handle(GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            var rowCount = await _userRepository.CountAsync(new UserSpec(request.Filter), cancellationToken);
            var users = await _userRepository.ListAsync(
                new UserSpec(request.Filter, request.GetSkip(), request.PageSize),
                cancellationToken);

            var roles = await _roleRepository.ListAsync(cancellationToken);

            var responseUsers = _mapper.Map<List<UserResponse>>(users);
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