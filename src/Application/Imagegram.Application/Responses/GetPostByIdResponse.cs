using System;

namespace Imagegram.Application.Responses
{
    public class GetPostByIdResponse
    {
        public Guid Id { get; }
        public string ImageUrl { get; }
        public GetPostByIdResponse(Guid id, string imageUrl)
        {
            Id = id;
            ImageUrl = imageUrl;
        }
    }
}
