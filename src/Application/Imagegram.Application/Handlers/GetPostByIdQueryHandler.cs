using AutoMapper;
using Imagegram.Application.Requests;
using Imagegram.Application.Responses;
using Imagegram.Core.Exceptions;
using Imagegram.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Application.Handlers
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdRequest, GetPostByIdResponse>
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public GetPostByIdQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        public async Task<GetPostByIdResponse> Handle(GetPostByIdRequest request, CancellationToken cancellationToken)
        {
            var post = await postRepository.GetByIdAsync(request.Id);
            if (post == null)
            {
                throw new NotFoundException("post");
            }
            return mapper.Map<GetPostByIdResponse>(post);
        }
    }
}
