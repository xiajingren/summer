using System;

namespace Summer.Application.UnitOfWork
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UnitOfWorkAttribute : Attribute
    {
    }
}