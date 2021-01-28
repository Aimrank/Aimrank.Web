namespace Aimrank.Common.Domain
{
    public interface IBusinessRule : IBusinessRuleBase
    {
        bool IsBroken();
    }
}