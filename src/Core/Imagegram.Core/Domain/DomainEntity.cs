using Imagegram.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imagegram.Core.Domain
{
    public abstract class DomainEntity : IDomainEntity
    {
        public Guid Id { get; set; }
        private List<IDomainEvent> domainEvents;
        public IEnumerable<IDomainEvent> DomainEvents => domainEvents.AsEnumerable();
        protected void CreateDomainEvent(IDomainEvent domainEvent)
        {
            domainEvents ??= new List<IDomainEvent>();
            domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            if (domainEvents != null)
            {
                domainEvents.Clear();
            }
        }

        protected void CheckDomainRule(IDomainRule domainRule)
        {
            if (!domainRule.IsValid())
            {
                throw new DomainRuleFailedException(domainRule);
            }
        }
    }
}
