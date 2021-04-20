using System;

namespace Imagegram.Application.Responses
{
    public class CreateAccountResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public CreateAccountResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
