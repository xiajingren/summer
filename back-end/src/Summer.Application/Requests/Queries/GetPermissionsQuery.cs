using System.Collections.Generic;
using MediatR;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
{
    public class GetPermissionsQuery : IRequest<IEnumerable<PermissionResponse>>
    {
        public int TargetId { get; set; }

        public int PermissionType { get; set; }

        public GetPermissionsQuery()
        {
        }

        public GetPermissionsQuery(int targetId, int permissionType)
        {
            TargetId = targetId;
            PermissionType = permissionType;
        }
    }
}