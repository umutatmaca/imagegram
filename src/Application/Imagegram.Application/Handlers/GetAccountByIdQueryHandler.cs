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
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdRequest, GetAccountByIdResponse>
    {
        private readonly IAccountRepository accountRepository;
        private readonly IMapper mapper;

        public GetAccountByIdQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.mapper = mapper;
        }

        public async Task<GetAccountByIdResponse> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.GetByIdAsync(request.Id);
            if (account == null)
            {
                throw new NotFoundException("account");
            }
            return mapper.Map<GetAccountByIdResponse>(account);
        }
    }
}
