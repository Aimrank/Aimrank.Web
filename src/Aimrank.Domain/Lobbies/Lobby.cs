using Aimrank.Common.Domain;
using Aimrank.Domain.Lobbies.Rules;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Domain.Lobbies
{
    public class Lobby : Entity
    {
        public LobbyId Id { get; }
        public MatchId MatchId { get; private set; }
        public LobbyStatus Status { get; private set; }
        public LobbyConfiguration Configuration { get; private set; } = new("aim_map");
        public HashSet<LobbyMember> Members { get; private set; } = new();
        
        private Lobby() {}

        private Lobby(LobbyId id, User user)
        {
            BusinessRules.Check(new UserMustHaveConnectedSteamRule(user));
            
            Id = id;
            Members.Add(new LobbyMember(user.Id, LobbyMemberRole.Leader));
        }

        public static async Task<Lobby> CreateAsync(LobbyId id, User user, ILobbyRepository lobbyRepository)
        {
            await BusinessRules.CheckAsync(new UserMustNotBeMemberOfAnyLobbyRule(lobbyRepository, user.Id));

            return new Lobby(id, user);
        }

        public async Task JoinAsync(User user, ILobbyRepository lobbyRepository)
        {
            await BusinessRules.CheckAsync(new UserMustNotBeMemberOfAnyLobbyRule(lobbyRepository, user.Id));
            BusinessRules.Check(new UserMustHaveConnectedSteamRule(user));
            
            Members.Add(new LobbyMember(user.Id, LobbyMemberRole.User));
        }

        public void Leave(UserId userId)
        {
            var member = Members.FirstOrDefault(m => m.UserId == userId);
            if (member is null)
            {
                return;
            }
            
            Members.Remove(member);

            if (member.Role == LobbyMemberRole.Leader && Members.Any())
            {
                var first = Members.FirstOrDefault();
                if (first is not null)
                {
                    Members.Remove(first);
                    Members.Add(new LobbyMember(first.UserId, LobbyMemberRole.Leader));
                }
            }
        }

        public void ChangeMap(UserId userId, string name)
        {
            BusinessRules.Check(new LobbyStatusMustMatchRule(this, LobbyStatus.Open));
            BusinessRules.Check(new UserMustBeLobbyLeaderRule(this, userId));
            BusinessRules.Check(new MapMustBeSupportedRule(name));
            
            Configuration = new LobbyConfiguration(name);
        }

        public void StartGame(MatchId matchId)
        {
            BusinessRules.Check(new LobbyStatusMustMatchRule(this, LobbyStatus.Closed));

            MatchId = matchId;
            Status = LobbyStatus.InGame;
        }

        public void Close(UserId userId)
        {
            BusinessRules.Check(new UserMustBeLobbyLeaderRule(this, userId));
            
            Status = LobbyStatus.Closed;
        }

        public void Open()
        {
            MatchId = null;
            Status = LobbyStatus.Open;
        }
    }
}