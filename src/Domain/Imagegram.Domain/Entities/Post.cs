using Imagegram.Core.Domain;
using Imagegram.Domain.Events;
using System;
using System.Collections.Generic;

namespace Imagegram.Domain.Entities
{
    public class Post : DomainEntity
    {
        public string ImageUrl { get; set; }
        public Account Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public static Post Create(Account creator, string imageUrl = null)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Creator = creator,
                ImageUrl = imageUrl
            };
            post.CreateDomainEvent(new PostCreatedDomainEvent(creator.Id, post.Id));

            return post;
        }

        public void SetImageUrl(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
    }
}
