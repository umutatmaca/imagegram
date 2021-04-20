using AutoMapper;
using Imagegram.Application.Requests;
using Imagegram.Application.Responses;
using Imagegram.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Application.Handlers
{
    public class GetAllPostsWithLastCommentsQueryHandler : IRequestHandler<GetAllPostsWithLastCommentsRequest, GetAllPostsWithLastCommentsResponse>
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public GetAllPostsWithLastCommentsQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllPostsWithLastCommentsResponse> Handle(GetAllPostsWithLastCommentsRequest request, CancellationToken cancellationToken)
        {
            var posts = postRepository.GetAllPostsWithComments(request.PageSize, request.PageNumber);
            return new GetAllPostsWithLastCommentsResponse(mapper.Map<IEnumerable<GetPostByIdWithCommentsResponse>>(posts));
        }
    }
}
