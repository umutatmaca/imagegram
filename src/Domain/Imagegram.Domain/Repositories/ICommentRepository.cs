using Imagegram.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imagegram.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(Comment comment);
        Task<Comment> GetPostCommentsByIdAsync(Guid postId, Guid commentId);
        Task<IEnumerable<Comment>> GetPostCommentsAsync(Guid postId, int pageSize = 50, int pageNumber = 1);
    }
}
