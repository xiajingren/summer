using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Apis.Role;
using Summer.Domain.Exceptions;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Apis.User.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IReadRepository<Domain.Entities.User> _userRepository;
        private readonly IReadRepository<Domain.Entities.Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IReadRepository<Domain.Entities.User> userRepository, IReadRepository<Domain.Entities.Role> roleRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundBusinessException();
            }

            var roles = await _roleRepository.ListAsync(new RoleByIdsSpec(user.RoleIds), cancellationToken);

            var response = _mapper.Map<UserResponse>(user);
            response.Roles = _mapper.Map<IEnumerable<RoleResponse>>(roles);

            return response;
        }
    }
}