using System.Collections.Generic;
using MediatR;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
{
    public class GetPermissionGroupsQuery : IRequest<IEnumerable<PermissionGroupInfoResponse>>
    {
    }
}