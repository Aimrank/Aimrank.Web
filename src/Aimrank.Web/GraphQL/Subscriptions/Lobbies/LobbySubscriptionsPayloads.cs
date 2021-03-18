using System.Collections.Generic;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Lobbies
{
    public record InvitationAcceptedPayload(Guid LobbyId, Guid InvitedPlayerId);
    
    public record InvitationCanceledPayload(Guid LobbyId, Guid InvitedPlayerId);
    
    public record LobbyConfigurationChangedPayload(Guid LobbyId, string Map, string Name, int Mode);

    public record MatchReadyPayload(Guid MatchId, string Map, DateTime ExpiresAt, IEnumerable<Guid> Lobbies);
}