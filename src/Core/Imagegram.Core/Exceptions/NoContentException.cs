using System;

namespace Imagegram.Core.Exceptions
{
    public class NoContentException : Exception
    {
        public string ResourceName { get; }
        public NoContentException(string resourceName) : base($"Content {resourceName} does not exist.")
        {
            ResourceName = resourceName;
        }
    }
}
