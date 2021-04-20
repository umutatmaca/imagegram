using System;

namespace Imagegram.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Reason { get; }
        public BadRequestException(string reason) : base($"Bad request, reason:{reason}.")
        {
            Reason = reason;
        }
    }
}
