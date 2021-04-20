using System;

namespace Imagegram.Application.Responses
{
    public class DeleteAccountResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public DeleteAccountResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
