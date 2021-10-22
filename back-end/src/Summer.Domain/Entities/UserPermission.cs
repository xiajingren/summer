﻿using System.Collections.Generic;
using Summer.Domain.SeedWork;

namespace Summer.Domain.Entities
{
    public class UserPermission : ValueObject
    {
        public int UserId { get; private set; }
        public string PermissionCode { get; private set; }

        private UserPermission()
        {
            // required by EF
        }

        public UserPermission(int userId, string permissionCode)
        {
            UserId = userId;
            PermissionCode = permissionCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return PermissionCode;
        }
    }
}