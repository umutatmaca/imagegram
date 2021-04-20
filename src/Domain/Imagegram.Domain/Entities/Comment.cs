using Imagegram.Core.Domain;
using Imagegram.Domain.Events;
using System;

namespace Imagegram.Domain.Entities
{
    public class Comment : DomainEntity
    {
        public string Content { get; set; }
        public Account Creator { get; set; }
        public Post Post { get; set; }
        public DateTime CreatedAt { get; set; }

        public static Comment Create(Account creator, Post post, string content)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Content = content,
                CreatedAt = DateTime.Now,
                Creator = creator,
                Post = post
            };
            comment.CreateDomainEvent(new CommentCreatedDomainEvent(creator.Id, post.Id, comment.Id));

            return comment;
        }
    }
}
