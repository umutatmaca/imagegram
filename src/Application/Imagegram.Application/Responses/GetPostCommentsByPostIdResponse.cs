using System.Collections.Generic;

namespace Imagegram.Application.Responses
{
    public class GetPostCommentsByPostIdResponse
    {
        public IEnumerable<GetPostCommentByIdResponse> Comments { get; }
        public GetPostCommentsByPostIdResponse(IEnumerable<GetPostCommentByIdResponse> comments)
        {
            Comments = comments;
        }
    }
}
