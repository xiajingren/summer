using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;
using Summer.Domain.Entities;
using Summer.Domain.Interfaces;

namespace Summer.Application.Requests.Handlers
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
                await _permissionManager.GetPermissionsAsync(request.TargetId, (PermissionType) request.PermissionType);

            return _mapper.Map<IEnumerable<PermissionResponse>>(permissions);
        }
    }
}