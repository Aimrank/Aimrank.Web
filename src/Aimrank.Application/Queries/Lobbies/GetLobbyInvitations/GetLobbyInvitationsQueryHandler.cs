using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Aimrank.Common.Application;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.Lobbies.GetLobbyInvitations
{
    public class GetLobbyInvitationsQueryHandler : IQueryHandler<GetLobbyInvitationsQuery, IEnumerable<LobbyInvitationDto>>
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
                    [I].[LobbyId] AS [LobbyId],
                    [U1].[Id] AS [InvitingUserId],
                    [U1].[UserName] AS [InvitingUserName],
                    [U2].[Id] AS [InvitedUserId],
                    [U2].[UserName] AS [InvitedUserName],
                    [I].[CreatedAt] AS [CreatedAt]
                FROM [aimrank].[LobbiesInvitations] AS [I]
                INNER JOIN [aimrank].[AspNetUsers] AS [U1] ON [U1].[Id] = [I].[InvitingUserId]
                INNER JOIN [aimrank].[AspNetUsers] AS [U2] ON [U2].[Id] = [I].[InvitedUserId]
                WHERE [I].[InvitedUserId] = @UserId;";

            var invitations =
                await connection.QueryAsync<LobbyInvitationDto>(sql, new {_executionContextAccessor.UserId});

            return invitations;
        }
    }
}