using System.Collections.Generic;
using System.Linq;
using MediatR;
using Summer.Application.Constants;
using Summer.Application.Permissions;

namespace Summer.Application.Apis.User.UpdateUser
{
    [Permission(nameof(UpdateUserCommand), "修改用户", PermissionConstants.UserGroupName)]
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<UpdateUserCommandRole> Roles { get; set; }

        public IEnumerable<int> RoleIds => Roles.Select(x => x.Id);

        public UpdateUserCommand(int id, string userName, IEnumerable<UpdateUserCommandRole> roles)
        {
            Id = id;
            UserName = userName;
            Roles = roles ?? new List<UpdateUserCommandRole>();
        }

        public class UpdateUserCommandRole
        {
            public int Id { get; set; }

            public UpdateUserCommandRole(int id)
            {
                Id = id;
            }
        }
    }
}