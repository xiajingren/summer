using System.Collections.Generic;
using Summer.Application.Apis.Role;

namespace Summer.Application.Apis.User
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<RoleResponse> Roles { get; set; }
    }
}