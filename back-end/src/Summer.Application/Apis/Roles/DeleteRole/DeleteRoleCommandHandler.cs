using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.SeedWork;

namespace Summer.Application.Apis.Roles.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IRepository<Role> _roleRepository;

        public DeleteRoleCommandHandler(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }


        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (role == null)
            {
                throw new NotFoundBusinessException();
            }

            await _roleRepository.DeleteAsync(role, cancellationToken);

            return Unit.Value;
        }
    }
}