using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Summer.Domain.Exceptions;
using Summer.Domain.Extensions;

namespace Summer.Application.Requests.Handlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleResponse>
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(RoleManager<IdentityRole<int>> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<IdentityRole<int>>(request);

            var existingRole = await _roleManager.FindByNameAsync(role.Name);
            if (existingRole != null)
            {
                throw new BusinessException("角色名已存在");
            }

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new DetailErrorsBusinessException(result.Errors.ToDetailErrors());
            }

            return _mapper.Map<RoleResponse>(role);
        }
    }
}