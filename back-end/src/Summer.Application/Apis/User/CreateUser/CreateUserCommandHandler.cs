using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Apis.Role;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Apis.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUserManager _userManager;
        private readonly IReadRepository<Domain.Entities.Role> _roleRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserManager userManager, IReadRepository<Domain.Entities.Role> roleRepository, IMapper mapper)
        {
            _userManager = userManager;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.CreateAsync(request.UserName, request.Password,
                request.Roles.Select(x => x.Id));

            var roles = await _roleRepository.ListAsync(new RoleByIdsSpec(user.RoleIds), cancellationToken);

            var response = _mapper.Map<UserResponse>(user);
            response.Roles = _mapper.Map<IEnumerable<RoleResponse>>(roles);

            return response;
        }
    }
}