using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;
using MediatR;
using Summer.Application.Responses;

namespace Summer.Application.Permissions
{
    public static class PermissionHelper
    {
        private static List<PermissionInfo> _permissions;

        public static IEnumerable<PermissionInfo> Permissions
        {
            get
            {
                if (_permissions != null)
                {
                    return _permissions;
                }

                _permissions = new List<PermissionInfo>();

                var attrs = Assembly.GetExecutingAssembly()?.DefinedTypes.Where(x =>
                        x.ImplementsGenericInterface(typeof(IRequest<>)) &&
                        x.GetCustomAttribute<PermissionAttribute>() != null)
                    .Select(x => x.GetCustomAttribute<PermissionAttribute>()).ToArray();


                foreach (var attr in attrs)
                {
                    _permissions.Add(new PermissionInfo(attr.Code, attr.Name, attr.GroupName));
                }

                _permissions = _permissions.OrderBy(x => x.GroupName).ToList();

                return _permissions;
            }
        }
    }
}