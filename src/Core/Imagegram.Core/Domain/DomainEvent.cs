using System;

namespace Imagegram.Core.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        public DateTime CreatedAt { get; }
        protected DomainEvent()
        {
            this.CreatedAt = DateTime.Now;
        }
    }
}
