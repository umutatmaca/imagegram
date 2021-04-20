using Imagegram.Application.Requests;
using Imagegram.Application.Responses;
using Imagegram.Core.Domain;
using Imagegram.Core.Exceptions;
using Imagegram.Domain.Entities;
using Imagegram.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Application.Handlers
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentRequest, CreateCommentResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICommentRepository commentRepository;
        private readonly IPostRepository postRepository;
        private readonly IAccountRepository accountRepository;

        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, ICommentRepository commentRepository, IPostRepository postRepository, IAccountRepository accountRepository)
        {
            this.unitOfWork = unitOfWork;
            this.commentRepository = commentRepository;
            this.postRepository = postRepository;
            this.accountRepository = accountRepository;
        }

        public async Task<CreateCommentResponse> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
        {
            var creator = await accountRepository.GetByIdAsync(request.CreatorId);
            if (creator == null)
            {
                throw new NotFoundException("creator account");
            }

            var post = await postRepository.GetByIdAsync(request.PostId);
            if (post == null)
            {
                throw new NotFoundException("post");
            }

            var comment = Comment.Create(creator, post, request.Content);
            await commentRepository.CreateCommentAsync(comment);

            await unitOfWork.CommitAsync(comment, cancellationToken);

            return new CreateCommentResponse(comment.Id, comment.Content);
        }
    }
}
