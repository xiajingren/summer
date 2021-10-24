using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
{
    [Permission(nameof(GetUsersQuery), "获取用户列表", PermissionConstants.UserGroupName)]
    public class GetUsersQuery : PaginationQuery, IRequest<PaginationResponse<UserResponse>>
    {
    }
}