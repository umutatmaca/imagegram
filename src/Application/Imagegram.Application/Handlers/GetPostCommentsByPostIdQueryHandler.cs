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
    public class GetPostCommentsByPostIdQueryHandler : IRequestHandler<GetPostCommentsByPostIdRequest, GetPostCommentsByPostIdResponse>
    {
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;

        public GetPostCommentsByPostIdQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }

        public async Task<GetPostCommentsByPostIdResponse> Handle(GetPostCommentsByPostIdRequest request, CancellationToken cancellationToken)
        {
            var comments = await commentRepository.GetPostCommentsAsync(request.PostId, request.PageSize, request.PageNumber);
            return new GetPostCommentsByPostIdResponse(mapper.Map<IEnumerable<GetPostCommentByIdResponse>>(comments));
        }
    }
}
