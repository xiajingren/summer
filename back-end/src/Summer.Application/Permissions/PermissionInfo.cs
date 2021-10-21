namespace Summer.Application.Permissions
{
    public class PermissionInfo
    {
        public string Code { get; }

        public string Name { get; }

        public string GroupName { get; }

        public PermissionInfo(string code, string name, string groupName = PermissionConstants.DefaultGroupName)
        {
            Code = code;
            Name = name;
            GroupName = groupName;
        }
    }
}