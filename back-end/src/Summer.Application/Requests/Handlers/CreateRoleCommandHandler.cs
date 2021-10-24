using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Requests.Commands;
using Summer.Application.Responses;
using Summer.Domain.Interfaces;

namespace Summer.Application.Requests.Handlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleResponse>
    {
        private readonly IRoleManager _roleManager;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IRoleManager roleManager, IMapper mapper)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RoleResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.CreateAsync(request.Name);
            
            return _mapper.Map<RoleResponse>(role);
        }
    }
}