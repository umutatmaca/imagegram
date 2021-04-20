using Imagegram.Domain.Entities;
using Imagegram.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Imagegram.Infrastructure.Database.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ImagegramContext imagegramContext;

        public AccountRepository(ImagegramContext imagegramContext)
        {
            this.imagegramContext = imagegramContext ?? throw new ArgumentNullException(nameof(imagegramContext));
        }

        public async Task CreateAsync(Account account)
        {
            await imagegramContext.AddAsync<Account>(account);
        }

        public void Delete(Account account)
        {
            imagegramContext.Remove<Account>(account);
        }

        public Task<Account> GetByIdAsync(Guid id)
        {
            return imagegramContext.Accounts.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
