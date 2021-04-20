using System;

namespace Imagegram.Application.Responses
{
    public class CreatePostResponse
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public CreatePostResponse(Guid id, string imageUrl)
        {
            Id = id;
            ImageUrl = imageUrl;
        }
    }
}
