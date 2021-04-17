namespace Aimrank.Web.Common.Domain
{
    public interface IBusinessRule : IBusinessRuleBase
    {
        bool IsBroken();
    }
}