using System.Threading.Tasks;

namespace Aimrank.Application.Contracts
{
    public interface IAimrankModule
    {
        Task ExecuteCommandAsync(ICommand command);
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);
        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}