using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Summer.Application.Requests.Commands;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;

namespace Summer.Application.Requests.Handlers
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly IRoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;

        public UpdateRoleCommandHandler(IRoleManager roleManager, IRepository<Role> roleRepository)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (role == null)
            {
                throw new NotFoundBusinessException();
            }

            await _roleManager.UpdateAsync(role, request.Name);

            return Unit.Value;
        }
    }
}