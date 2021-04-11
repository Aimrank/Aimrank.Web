using System.Threading.Tasks;

namespace Aimrank.Modules.CSGO.Application.Contracts
{
    public interface ICSGOModule
    {
        Task ExecuteCommandAsync(ICommand command);
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);
        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}