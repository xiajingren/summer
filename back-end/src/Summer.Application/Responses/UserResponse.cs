using System.Collections.Generic;

namespace Summer.Application.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<RoleResponse> Roles { get; set; }
    }
}