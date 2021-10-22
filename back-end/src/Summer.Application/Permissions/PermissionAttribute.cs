using System;

namespace Summer.Application.Permissions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PermissionAttribute : Attribute
    {
        public string Code { get; }

        public string Name { get; }

        public string GroupName { get; }

        public int Sort { get; }

        public PermissionAttribute(string code, string name, string groupName, int sort = default)
        {
            Code = code;
            Name = name;
            GroupName = groupName;
            Sort = sort;
        }
    }
}