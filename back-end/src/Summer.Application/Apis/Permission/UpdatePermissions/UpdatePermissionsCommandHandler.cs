using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Permissions;
using Summer.Domain.Entities;
using Summer.Domain.Exceptions;
using Summer.Domain.SeedWork;
using Summer.Domain.Specifications;

namespace Summer.Application.Apis.Permission.UpdatePermissions
{
    public class UpdatePermissionsCommandHandler : IRequestHandler<UpdatePermissionsCommand>
    {
        private readonly IRepository<Domain.Entities.Permission> _permissionRepository;
        private readonly IReadRepository<Domain.Entities.User> _userRepository;
        private readonly IReadRepository<Domain.Entities.Role> _roleRepository;

        public UpdatePermissionsCommandHandler(IRepository<Domain.Entities.Permission> permissionRepository,
            IReadRepository<Domain.Entities.User> userRepository, IReadRepository<Domain.Entities.Role> roleRepository)
        {
            _permissionRepository = permissionRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(UpdatePermissionsCommand request, CancellationToken cancellationToken)
        {
            if (request.PermissionType == PermissionType.User)
            {
                var user = await _userRepository.GetByIdAsync(request.TargetId, cancellationToken);
                if (user == null)
                    throw new BusinessException();
            }

            if (request.PermissionType == PermissionType.Role)
            {
                var role = await _roleRepository.GetByIdAsync(request.TargetId, cancellationToken);
                if (role == null)
                    throw new BusinessException();
            }

            var olds = await _permissionRepository.ListAsync(new PermissionSpec(request.TargetId,
                request.PermissionType), cancellationToken);
            await _permissionRepository.DeleteRangeAsync(olds, cancellationToken);

            foreach (var code in request.PermissionCodes)
            {
                if (!PermissionHelper.PermissionCodes.Contains(code))
                    continue;

                await _permissionRepository.AddAsync(
                    new Domain.Entities.Permission(request.TargetId, request.PermissionType, code), cancellationToken);
            }

            return Unit.Value;
        }
    }
}