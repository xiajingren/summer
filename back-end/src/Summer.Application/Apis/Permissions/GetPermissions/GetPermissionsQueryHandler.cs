using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Domain.Interfaces;

namespace Summer.Application.Apis.Permissions.GetPermissions
{
    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, IEnumerable<PermissionResponse>>
    {
        private readonly IPermissionManager _permissionManager;
        private readonly IMapper _mapper;

        public GetPermissionsQueryHandler(IPermissionManager permissionManager, IMapper mapper)
        {
            _permissionManager = permissionManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionResponse>> Handle(GetPermissionsQuery request,
            CancellationToken cancellationToken)
        {
            var permissions =
                await _permissionManager.GetPermissionsAsync(request.TargetId, request.PermissionType);

            return _mapper.Map<IEnumerable<PermissionResponse>>(permissions);
        }
    }
}