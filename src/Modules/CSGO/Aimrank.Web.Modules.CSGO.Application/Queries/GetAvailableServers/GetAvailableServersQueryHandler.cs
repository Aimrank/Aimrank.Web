using Aimrank.Web.Modules.CSGO.Application.Contracts;
using Aimrank.Web.Modules.CSGO.Application.Repositories;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.CSGO.Application.Queries.GetAvailableServers
{
    internal class GetAvailableServersQueryHandler : IQueryHandler<GetAvailableServersQuery, int>
    {
        private readonly IPodRepository _podRepository;

        public GetAvailableServersQueryHandler(IPodRepository podRepository)
        {
            _podRepository = podRepository;
        }

        public Task<int> Handle(GetAvailableServersQuery request, CancellationToken cancellationToken)
            => _podRepository.GetAvailableServersCountAsync();
    }
}