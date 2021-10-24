using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Queries
{
    [Permission(nameof(GetUserByIdQuery), "根据Id获取用户", PermissionConstants.UserGroupName)]
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}