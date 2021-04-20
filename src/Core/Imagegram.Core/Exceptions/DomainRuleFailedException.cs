using Imagegram.Core.Domain;
using System;

namespace Imagegram.Core.Exceptions
{
    public class DomainRuleFailedException : Exception
    {
        public IDomainRule FailedRule { get; }
        public DomainRuleFailedException(IDomainRule domainRule) : base(domainRule.Message)
        {
            this.FailedRule = domainRule;
        }

        public override string ToString()
        {
            return $"{FailedRule.GetType().FullName}: {FailedRule.Message}";
        }
    }
}
