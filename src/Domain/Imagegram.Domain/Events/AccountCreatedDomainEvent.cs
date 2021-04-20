using Imagegram.Core.Domain;
using System;

namespace Imagegram.Domain.Events
{
    public class AccountCreatedDomainEvent : DomainEvent
    {
        public Guid AccountId { get; }
        public AccountCreatedDomainEvent(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
