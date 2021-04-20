using System;

namespace Imagegram.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public string ResourceName { get; }
        public NotFoundException(string resourceName) : base($"{resourceName} not found.")
        {
            this.ResourceName = resourceName;
        }
    }
}
