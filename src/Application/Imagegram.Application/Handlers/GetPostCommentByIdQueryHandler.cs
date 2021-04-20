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
    public class GetPostCommentByIdQueryHandler : IRequestHandler<GetPostCommentByIdRequest, GetPostCommentByIdResponse>
    {
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;

        public GetPostCommentByIdQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }

        public async Task<GetPostCommentByIdResponse> Handle(GetPostCommentByIdRequest request, CancellationToken cancellationToken)
        {
            var comment = await commentRepository.GetPostCommentsByIdAsync(request.PostId, request.CommentId);
            if (comment == null)
            {
                throw new NotFoundException("comment");
            }
            return mapper.Map<GetPostCommentByIdResponse>(comment);
        }
    }
}
