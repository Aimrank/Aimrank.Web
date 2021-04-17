using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Cluster.Application.Contracts
{
    public interface IClusterModule
    {
        Task ExecuteCommandAsync(ICommand command);
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);
        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}