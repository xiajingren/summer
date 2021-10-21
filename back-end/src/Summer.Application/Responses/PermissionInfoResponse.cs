﻿namespace Summer.Application.Responses
{
    public class PermissionInfoResponse
    {
        public string Code { get; }

        public string Name { get; }

        public PermissionInfoResponse(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}