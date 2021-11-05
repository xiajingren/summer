using System.Collections.Generic;
using Summer.Application.Apis.Roles;

namespace Summer.Application.Apis.Users
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<RoleResponse> Roles { get; set; }
    }
}