using Imagegram.Core.Domain;
using System;

namespace Imagegram.Domain.Events
{
    public class CommentCreatedDomainEvent : DomainEvent
    {
        public Guid AccountId { get; }
        public Guid PostId { get; }
        public Guid CommentId { get; }
        public CommentCreatedDomainEvent(Guid accountId, Guid postId, Guid commentId)
        {
            AccountId = accountId;
            PostId = postId;
            CommentId = commentId;
        }
    }
}
