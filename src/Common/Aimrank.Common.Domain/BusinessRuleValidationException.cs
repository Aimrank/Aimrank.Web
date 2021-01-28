using System;

namespace Aimrank.Common.Domain
{
    public class BusinessRuleValidationException : Exception
    {
        public IBusinessRuleBase BrokenRule { get; }
        
        public BusinessRuleValidationException(IBusinessRuleBase brokenRule) : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
        }
    }
}