using System;

namespace Imagegram.Core.Exceptions
{
    public class AuthenticationException : Exception
    {
        public string Reason { get; }
        public AuthenticationException(string reason) : base(reason)
        {
            this.Reason = reason;
        }
    }
}
