﻿using Ardalis.Specification;

namespace Summer.Domain.SeedWork
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}