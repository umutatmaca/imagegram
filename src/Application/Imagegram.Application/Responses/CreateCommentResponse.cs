using System;

namespace Imagegram.Application.Responses
{
    public class CreateCommentResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public CreateCommentResponse(Guid id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}
