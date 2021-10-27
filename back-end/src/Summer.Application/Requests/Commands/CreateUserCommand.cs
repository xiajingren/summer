using System.Collections.Generic;
using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;
using Summer.Application.Responses;
using Summer.Application.UnitOfWork;

namespace Summer.Application.Requests.Commands
{
    [Permission(nameof(CreateUserCommand), "创建用户", PermissionConstants.UserGroupName)]
    public class CreateUserCommand : IRequest<UserResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public IEnumerable<CreateUserCommandRole> Roles { get; set; }

        public CreateUserCommand(string userName, string password, IEnumerable<CreateUserCommandRole> roles)
        {
            UserName = userName;
            Password = password;
            Roles = roles ?? new List<CreateUserCommandRole>();
        }

        public class CreateUserCommandRole
        {
            public int Id { get; set; }

            public CreateUserCommandRole(int id)
            {
                Id = id;
            }
        }
    }
}