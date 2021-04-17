using System.Threading.Tasks;

namespace Aimrank.Web.Common.Domain
{
    public interface IAsyncBusinessRule : IBusinessRuleBase
    {
        Task<bool> IsBrokenAsync();
    }
}