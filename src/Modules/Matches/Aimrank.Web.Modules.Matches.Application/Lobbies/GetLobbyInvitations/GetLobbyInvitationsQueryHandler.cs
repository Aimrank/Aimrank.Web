using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyInvitations
{
    internal class GetLobbyInvitationsQueryHandler : IQueryHandler<GetLobbyInvitationsQuery, IEnumerable<LobbyInvitationDto>>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetLobbyInvitationsQueryHandler(
            IExecutionContextAccessor executionContextAccessor,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _executionContextAccessor = executionContextAccessor;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<LobbyInvitationDto>> Handle(GetLobbyInvitationsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT
                    [I].[LobbyId],
                    [I].[InvitingPlayerId],
                    [I].[InvitedPlayerId],
                    [I].[CreatedAt]
                FROM [matches].[LobbiesInvitations] AS [I]
                WHERE [I].[InvitedPlayerId] = @PlayerId;";

            var invitations =
                await connection.QueryAsync<LobbyInvitationDto>(sql, new {PlayerId = _executionContextAccessor.UserId});

            return invitations;
        }
    }
}