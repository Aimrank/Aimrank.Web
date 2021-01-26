using Aimrank.Application.CSGO;
using Aimrank.Application.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.GetServerProcesses
{
    public class GetServerProcessesQueryHandler : IQueryHandler<GetServerProcessesQuery, IEnumerable<ServerProcessDto>>
    {
        private readonly IServerProcessManager _serverProcessManager;

        public GetServerProcessesQueryHandler(IServerProcessManager serverProcessManager)
        {
            _serverProcessManager = serverProcessManager;
        }

        public Task<IEnumerable<ServerProcessDto>> Handle(GetServerProcessesQuery request,
            CancellationToken cancellationToken)
            => Task.FromResult(_serverProcessManager.GetProcesses());
    }
}