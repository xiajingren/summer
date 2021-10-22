using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class RolePermission : BaseEntity, IAggregateRoot
    {
        public int RoleId { get; private set; }
        public string PermissionCode { get; private set; }

        // private RolePermission()
        // {
        //     // required by EF
        // }

        public RolePermission(int roleId, string permissionCode)
        {
            RoleId = roleId;
            PermissionCode = permissionCode;
        }
    }
}