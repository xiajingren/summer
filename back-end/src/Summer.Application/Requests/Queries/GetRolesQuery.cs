using System.Collections.Generic;
using MediatR;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
{
    public class GetRolesQuery : PaginationQuery, IRequest<PaginationResponse<RoleResponse>>
    {
        public GetRolesQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
        {
        }
    }
}