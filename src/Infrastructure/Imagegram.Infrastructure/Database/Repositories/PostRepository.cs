using Imagegram.Domain.Entities;
using Imagegram.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imagegram.Infrastructure.Database.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ImagegramContext imagegramContext;

        public PostRepository(ImagegramContext imagegramContext)
        {
            this.imagegramContext = imagegramContext ?? throw new ArgumentNullException(nameof(imagegramContext));
        }

        public async Task CreateAsync(Post post)
        {
            await imagegramContext.Posts.AddAsync(post);
        }

        public void Update(Post post)
        {
            imagegramContext.Posts.Update(post);
        }

        public Task<Post> GetByIdAsync(Guid id)
        {
            return imagegramContext.Posts
                                   .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Post> GetAllPostsWithComments(int pageSize = 50, int pageNumber = 1)
        {
            return imagegramContext.Posts
                                   .Include(x => x.Creator)
                                   .Include(x => x.Comments)
                                        .ThenInclude(c => c.Creator)
                                   .OrderByDescending(x => x.Comments.Count())
                                   .Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .Select(c => new Post
                                   {
                                       Comments = c.Comments.OrderByDescending(c => c.CreatedAt).Take(3),
                                       CreatedAt = c.CreatedAt,
                                       Creator = c.Creator,
                                       Id = c.Id,
                                       ImageUrl = c.ImageUrl
                                   })
                                   .AsNoTracking()
                                   .ToList();
        }

        public IEnumerable<Post> GetAccountPosts(Guid creatorId)
        {
            return imagegramContext.Posts.Include(x => x.Creator)
                .Where(x => x.Creator.Id == creatorId)
                .AsNoTracking()
                .ToList();
        }
    }
}
