using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Requests.Commands
{
    [Permission(nameof(UpdateUserPasswordCommand), "修改用户密码", PermissionConstants.UserGroupName)]
    public class UpdateUserPasswordCommand : IRequest
    {
        public int Id { get; set; }

        public string Password { get; set; }


        public UpdateUserPasswordCommand(int id, string password)
        {
            Id = id;
            Password = password;
        }
    }
}