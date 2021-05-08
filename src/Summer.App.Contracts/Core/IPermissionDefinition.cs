using System.Collections.Generic;

namespace Summer.App.Contracts.Core
{
    public interface IPermissionDefinition
    {
        public IEnumerable<Permission> Define(IEnumerable<Permission> permissions)
        {
            return permissions;
        }

    }
}