using Aimrank.Application.CSGO;
using Aimrank.Application.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.GetServerProcesses
{
    public class GetServerProcessQueryHandler : IQueryHandler<GetServerProcessQuery, ServerProcessDto>
    {
        private readonly IServerProcessManager _serverProcessManager;

        public GetServerProcessQueryHandler(IServerProcessManager serverProcessManager)
        {
            _serverProcessManager = serverProcessManager;
        }

        public Task<ServerProcessDto> Handle(GetServerProcessQuery request, CancellationToken cancellationToken)
            => Task.FromResult(_serverProcessManager.GetProcesses()
                .FirstOrDefault(p => p.ServerId == request.ServerId));
    }
}