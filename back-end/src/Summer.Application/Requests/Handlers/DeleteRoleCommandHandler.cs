using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Summer.Application.Requests.Commands;
using Summer.Domain.Exceptions;
using Summer.Domain.Extensions;

namespace Summer.Application.Requests.Handlers
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
            {
                throw new NotFoundBusinessException();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new DetailErrorsBusinessException(result.Errors.ToDetailErrors());
            }

            return Unit.Value;
        }
    }
}