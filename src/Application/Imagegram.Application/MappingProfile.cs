using AutoMapper;
using Imagegram.Application.Responses;
using Imagegram.Domain.Entities;

namespace Imagegram.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, GetPostByIdResponse>();
            CreateMap<Post, GetPostByIdWithCommentsResponse>();
            CreateMap<Comment, GetPostCommentByIdResponse>();
            CreateMap<Account, GetAccountByIdResponse>();
        }
    }
}
