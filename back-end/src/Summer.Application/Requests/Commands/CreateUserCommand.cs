using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;
using Summer.Application.Responses;

namespace Summer.Application.Requests.Commands
{
    [Permission(nameof(CreateUserCommand), "创建用户", PermissionConstants.UserGroupName)]
    public class CreateUserCommand : IRequest<UserResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public CreateUserCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}