using Imagegram.Application.Requests;
using Imagegram.Application.Responses;
using Imagegram.Core.Domain;
using Imagegram.Domain.Repositories;
using Imagegram.Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Application.Handlers
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountRequest, DeleteAccountResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAccountRepository accountRepository;
        private readonly IPostRepository postRepository;
        private readonly IFileUploader fileUploader;

        public DeleteAccountCommandHandler(IUnitOfWork unitOfWork, IAccountRepository accountRepository, IPostRepository postRepository, IFileUploader fileUploader)
        {
            this.unitOfWork = unitOfWork;
            this.accountRepository = accountRepository;
            this.postRepository = postRepository;
            this.fileUploader = fileUploader;
        }

        public async Task<DeleteAccountResponse> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.GetByIdAsync(request.Id);
            if (account == null)
            {
                return new DeleteAccountResponse(Guid.Empty, "");
            }

            var posts = postRepository.GetAccountPosts(account.Id);
            foreach (var post in posts)
            {
                fileUploader.DeleteFolder(post.Id.ToString());
            }

            accountRepository.Delete(account);
            await unitOfWork.CommitAsync(account, cancellationToken);

            return new DeleteAccountResponse(account.Id, account.Name);
        }
    }
}
