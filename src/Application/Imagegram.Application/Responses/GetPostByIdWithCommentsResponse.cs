using System;
using System.Collections.Generic;

namespace Imagegram.Application.Responses
{
    public class GetPostByIdWithCommentsResponse
    {
        public Guid Id { get; }
        public string ImageUrl { get; }
        public DateTime CreatedAt { get; }
        public GetAccountByIdResponse Creator { get; }
        public IEnumerable<GetPostCommentByIdResponse> Comments { get; }
        public GetPostByIdWithCommentsResponse(Guid id, string imageUrl, DateTime createdAt, GetAccountByIdResponse creator, IEnumerable<GetPostCommentByIdResponse> comments)
        {
            Id = id;
            ImageUrl = imageUrl;
            Comments = comments;
            CreatedAt = createdAt;
            Creator = creator;
        }
    }
}
