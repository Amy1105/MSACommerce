using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Domain
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        private readonly List<BaseEvent> _domainEvents = [];

        [NotMapped] public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public TId Id { get; set; } = default!;

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
