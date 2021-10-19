using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Summer.Application.Requests.Commands;
using Summer.Shared.Exceptions;

namespace Summer.Application.Requests.Handlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(RoleManager<IdentityRole<int>> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<IdentityRole<int>>(request);
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new FriendlyException("创建失败");
            }

            return Unit.Value;
        }
    }
}