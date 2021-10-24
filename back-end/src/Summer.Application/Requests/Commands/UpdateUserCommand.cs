using System.Collections.Generic;
using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Requests.Commands
{
    [Permission(nameof(UpdateUserCommand), "修改用户", PermissionConstants.UserGroupName)]
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public IEnumerable<int> RoleIds { get; set; } = new List<int>();
        
        public UpdateUserCommand(int id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }
    }
}