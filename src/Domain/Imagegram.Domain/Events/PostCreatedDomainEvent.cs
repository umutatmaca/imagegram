using Imagegram.Core.Domain;
using System;

namespace Imagegram.Domain.Events
{
    public class PostCreatedDomainEvent : DomainEvent
    {
        public Guid AccountId { get; }
        public Guid PostId { get; }
        public PostCreatedDomainEvent(Guid accountId, Guid postId)
        {
            AccountId = accountId;
            PostId = postId;
        }
    }
}
