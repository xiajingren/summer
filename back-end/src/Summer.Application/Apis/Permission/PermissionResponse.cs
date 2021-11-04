using Summer.Domain.Entities;

namespace Summer.Application.Apis.Permission
{
    public class PermissionResponse
    {
        public int TargetId { get; set; }

        public PermissionType PermissionType { get; set; }

        public string PermissionCode { get; set; }
    }
}