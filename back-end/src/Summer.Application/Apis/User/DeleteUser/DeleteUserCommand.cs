using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.User.DeleteUser
{
    [Permission(nameof(DeleteUserCommand), "删除用户", PermissionConstants.UserGroupName)]
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}