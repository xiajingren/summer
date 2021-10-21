﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Summer.Application.Permissions;
using Summer.Application.Requests.Queries;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Handlers
{
    public class GetPermissionGroupsQueryHandler : IRequestHandler<GetPermissionGroupsQuery,
        IEnumerable<PermissionGroupInfoResponse>>
    {
        public Task<IEnumerable<PermissionGroupInfoResponse>> Handle(GetPermissionGroupsQuery request,
            CancellationToken cancellationToken)
        {
            var groups = PermissionHelper.Permissions.GroupBy(x => x.GroupName);

            var list = groups.Select(@group =>
                new PermissionGroupInfoResponse(@group.Key).AddPermissions(@group.Select(x =>
                    new PermissionInfoResponse(x.Code, x.Name))));

            return Task.FromResult(list);
        }
    }
}