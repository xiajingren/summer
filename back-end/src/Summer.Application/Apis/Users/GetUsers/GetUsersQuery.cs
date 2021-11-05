using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.Users.GetUsers
{
    [Permission(nameof(GetUsersQuery), "获取用户列表", PermissionConstants.UserGroupName)]
    public class GetUsersQuery : PaginationQuery, IRequest<PaginationResponse<UserResponse>>
    {
    }
}