using Imagegram.Domain.Entities;
using Imagegram.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imagegram.Infrastructure.Database.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ImagegramContext imagegramContext;

        public CommentRepository(ImagegramContext imagegramContext)
        {
            this.imagegramContext = imagegramContext ?? throw new ArgumentNullException(nameof(imagegramContext));
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            await imagegramContext.Comments.AddAsync(comment);
        }

        public async Task<IEnumerable<Comment>> GetPostCommentsAsync(Guid postId, int pageSize = 50, int pageNumber = 1)
        {
            var comments = await imagegramContext.Comments.Include(x => x.Creator)
                                                          .Where(x => x.Post.Id == postId)
                                                          .OrderByDescending(x => x.CreatedAt)
                                                          .Skip((pageNumber - 1) * pageSize)
                                                          .Take(pageSize)
                                                          .AsNoTracking()
                                                          .ToListAsync();
            return comments;
        }

        public async Task<Comment> GetPostCommentsByIdAsync(Guid postId, Guid commentId)
        {
            var post = await imagegramContext.Comments.Include(x => x.Creator)
                                                      .Where(x => x.Post.Id == postId)
                                                      .SingleOrDefaultAsync(x => x.Id == commentId);
            return post;
        }
    }
}
