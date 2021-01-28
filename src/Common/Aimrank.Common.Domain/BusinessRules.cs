using System.Threading.Tasks;

namespace Aimrank.Common.Domain
{
    public static class BusinessRules
    {
        public static void Check(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        public static async Task CheckAsync(IAsyncBusinessRule rule)
        {
            if (await rule.IsBrokenAsync())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}