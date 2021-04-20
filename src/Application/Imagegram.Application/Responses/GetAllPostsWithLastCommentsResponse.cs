using System.Collections.Generic;

namespace Imagegram.Application.Responses
{
    public class GetAllPostsWithLastCommentsResponse
    {
        public IEnumerable<GetPostByIdWithCommentsResponse> Posts { get; }
        public GetAllPostsWithLastCommentsResponse(IEnumerable<GetPostByIdWithCommentsResponse> posts)
        {
            Posts = posts;
        }
    }
}
