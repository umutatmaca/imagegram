using System;

namespace Imagegram.Application.Responses
{
    public class GetPostCommentByIdResponse
    {
        public Guid Id { get; }
        public string Content { get; }
        public DateTime CreatedAt { get; }
        public GetAccountByIdResponse Creator { get; }
        public GetPostCommentByIdResponse(Guid id, string content, DateTime createdAt, GetAccountByIdResponse creator)
        {
            Id = id;
            Content = content;
            CreatedAt = createdAt;
            Creator = creator;
        }
    }
}
