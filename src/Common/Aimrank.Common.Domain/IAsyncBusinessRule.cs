using System.Threading.Tasks;

namespace Aimrank.Common.Domain
{
    public interface IAsyncBusinessRule : IBusinessRuleBase
    {
        Task<bool> IsBrokenAsync();
    }
}