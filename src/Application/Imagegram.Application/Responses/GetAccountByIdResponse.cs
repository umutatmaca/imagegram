using System;

namespace Imagegram.Application.Responses
{
    public class GetAccountByIdResponse
    {
        /// <summary>
        /// account id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// account name
        /// </summary>
        public string Name { get; }
        public GetAccountByIdResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
