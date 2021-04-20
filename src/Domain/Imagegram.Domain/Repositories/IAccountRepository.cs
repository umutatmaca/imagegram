using Imagegram.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Imagegram.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task CreateAsync(Account account);

        Task<Account> GetByIdAsync(Guid id);

        void Delete(Account account);
    }
}
