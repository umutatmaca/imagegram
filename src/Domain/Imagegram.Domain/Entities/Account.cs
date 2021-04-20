using Imagegram.Core.Domain;
using Imagegram.Domain.Events;
using System;
using System.Collections.Generic;

namespace Imagegram.Domain.Entities
{
    public class Account : DomainEntity
    {
        public string Name { get; private set; }
        public IEnumerable<Post> Posts { get; private set; }
        public IEnumerable<Comment> Comments { get; private set; }
        public static Account Create(string name)
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Name = name
            };
            account.CreateDomainEvent(new AccountCreatedDomainEvent(account.Id));

            return account;
        }
    }
}
