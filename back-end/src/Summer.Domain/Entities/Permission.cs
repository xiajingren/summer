using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class Permission : BaseEntity, IAggregateRoot
    {
        public int TargetId { get; private set; }

        public PermissionType PermissionType { get; private set; } // todo: 枚举类修改

        public string PermissionCode { get; private set; }

        public Permission(int targetId, PermissionType permissionType, string permissionCode)
        {
            TargetId = targetId;
            PermissionType = permissionType;
            PermissionCode = permissionCode;
        }
    }

    public enum PermissionType
    {
        User,
        Role,
    }
}