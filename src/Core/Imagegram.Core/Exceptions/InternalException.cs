using System;

namespace Imagegram.Core.Exceptions
{
    public class InternalException : Exception
    {
        public string Reason { get; }
        public InternalException(string reason) : base(reason)
        {
            this.Reason = reason;
        }
    }
}
