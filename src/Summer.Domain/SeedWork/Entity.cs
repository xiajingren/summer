using System.Collections.Generic;
using MediatR;

namespace Summer.Domain.SeedWork
{
    public abstract class Entity
    {
        private List<INotification> _domainEvents;
        public virtual int Id { get; protected set; }
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}