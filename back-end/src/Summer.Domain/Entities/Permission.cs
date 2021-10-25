using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class Permission : BaseEntity, IAggregateRoot
    {
        public int TargetId { get; set; }

        public string PermissionCode { get; set; }

        public PermissionType PermissionType { get; set; }
    }

    public enum PermissionType
    {
        User,
        Role,
    }
}