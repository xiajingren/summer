using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Apis.Roles;
using Summer.Domain.Entities;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Apis.Users.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserResponse>
    {
        private readonly IUserManager _userManager;
        private readonly IReadRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IUserManager userManager, IReadRepository<Role> roleRepository, IMapper mapper)
        {
            _userManager = userManager;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.CreateAsync(request.UserName, request.Password);

            var roles = await _roleRepository.ListAsync(new RoleByIdsSpec(user.RoleIds), cancellationToken);

            var response = _mapper.Map<UserResponse>(user);
            response.Roles = _mapper.Map<IEnumerable<RoleResponse>>(roles);

            return response;
        }
    }
}