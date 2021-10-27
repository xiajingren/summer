using System;
using MediatR;

namespace Summer.Domain.SeedWork
{
    public class BaseEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}