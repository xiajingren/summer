using System;

namespace Summer.Application.Permissions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PermissionAttribute : Attribute
    {
        public string Code { get; }

        public string Name { get; }

        public string GroupName { get; }

        public PermissionAttribute(string code, string name, string groupName = "default")
        {
            Code = code;
            Name = name;
            GroupName = groupName;
        }
    }
}