using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Summer.Application.Requests.Commands;
using Summer.Domain.Exceptions;

namespace Summer.Application.Requests.Handlers
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(RoleManager<IdentityRole<int>> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
            {
                throw new NotFoundBusinessException();
            }

            var newRole = _mapper.Map(request, role);
            var result = await _roleManager.UpdateAsync(newRole);
            if (!result.Succeeded)
            {
                throw new DetailErrorsBusinessException(result.Errors.ToDetailErrors());
            }

            return Unit.Value;
        }
    }
}