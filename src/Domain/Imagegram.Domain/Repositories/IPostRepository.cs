using Imagegram.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imagegram.Domain.Repositories
{
    public interface IPostRepository
    {
        Task CreateAsync(Post post);
        Task<Post> GetByIdAsync(Guid id);
        IEnumerable<Post> GetAllPostsWithComments(int pageSize = 50, int pageNumber = 1);
        IEnumerable<Post> GetAccountPosts(Guid creatorId);
        void Update(Post post);
    }
}
