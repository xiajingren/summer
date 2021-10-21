using System.Collections.Generic;
using Summer.Application.Permissions;

namespace Summer.Application.Responses
{
    public class PermissionGroupInfoResponse
    {
        public string GroupName { get; }

        public List<PermissionInfoResponse> Permissions { get; } = new();

        public PermissionGroupInfoResponse(string groupName)
        {
            GroupName = groupName;
        }

        public PermissionGroupInfoResponse AddPermission(string code, string name)
        {
            Permissions.Add(new PermissionInfoResponse(code, name));
            return this;
        }

        public PermissionGroupInfoResponse AddPermission(PermissionInfoResponse permission)
        {
            Permissions.Add(permission);
            return this;
        }

        public PermissionGroupInfoResponse AddPermissions(IEnumerable<PermissionInfoResponse> permissions)
        {
            Permissions.AddRange(permissions);
            return this;
        }
    }
}