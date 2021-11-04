using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Domain.Exceptions;
using Summer.Domain.Interfaces;
using Summer.Domain.SeedWork;

namespace Summer.Application.Apis.Role.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly IRoleManager _roleManager;
        private readonly IReadRepository<Domain.Entities.Role> _roleRepository;

        public UpdateRoleCommandHandler(IRoleManager roleManager, IReadRepository<Domain.Entities.Role> roleRepository)
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
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