using Imagegram.Application.Requests;
using Imagegram.Application.Responses;
using Imagegram.Core.Domain;
using Imagegram.Core.Exceptions;
using Imagegram.Domain.Entities;
using Imagegram.Domain.Repositories;
using Imagegram.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Application.Handlers
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostRequest, CreatePostResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IImageUploader imageUploader;
        private readonly IPostRepository postRepository;
        private readonly IAccountRepository accountRepository;

        public CreatePostCommandHandler(IUnitOfWork unitOfWork, IImageUploader imageUploader, IPostRepository postRepository, IAccountRepository accountRepository)
        {
            this.unitOfWork = unitOfWork;
            this.imageUploader = imageUploader;
            this.postRepository = postRepository;
            this.accountRepository = accountRepository;
        }

        public async Task<CreatePostResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.GetByIdAsync(request.AccountId);
            if (account == null)
            {
                throw new NotFoundException("account");
            }

            var post = Post.Create(account);
            var imageUrl = await imageUploader.UploadAsync(post.Id, request.ImageFile, cancellationToken);
            post.SetImageUrl(imageUrl);

            await postRepository.CreateAsync(post);

            await unitOfWork.CommitAsync(account, cancellationToken);

            return new CreatePostResponse(post.Id, imageUrl);
        }
    }
}
