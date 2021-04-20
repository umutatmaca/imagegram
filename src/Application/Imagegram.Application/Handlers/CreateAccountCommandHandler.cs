using Imagegram.Application.Requests;
using Imagegram.Application.Responses;
using Imagegram.Core.Domain;
using Imagegram.Domain.Entities;
using Imagegram.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Application.Handlers
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountRequest, CreateAccountResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAccountRepository accountRepository;

        public CreateAccountCommandHandler(IUnitOfWork unitOfWork, IAccountRepository accountRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accountRepository = accountRepository;
        }

        public async Task<CreateAccountResponse> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            var account = Account.Create(request.Name);
            await accountRepository.CreateAsync(account);
            await unitOfWork.CommitAsync(account, cancellationToken);

            return new CreateAccountResponse(account.Id, account.Name);
        }
    }
}
