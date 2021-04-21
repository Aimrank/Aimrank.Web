using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Lobbies.Events;
using Aimrank.Web.Modules.Matches.Domain.Lobbies.Rules;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using Aimrank.Web.Modules.Matches.Domain.Players;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies
{
    public class Lobby : Entity, IAggregateRoot
    {
        private const int MaxMembers = 2;
        
        public LobbyId Id { get; }
        public LobbyStatus Status { get; private set; }
        public LobbyConfiguration Configuration { get; private set; }
        private readonly HashSet<LobbyMember> _members = new();
        private readonly HashSet<LobbyInvitation> _invitations = new();

        public IEnumerable<LobbyMember> Members
        {
            get => _members;
            private init => _members = new HashSet<LobbyMember>(value);
        }

        public IEnumerable<LobbyInvitation> Invitations
        {
            get => _invitations;
            private init => _invitations = new HashSet<LobbyInvitation>(value);
        }
        
        private Lobby() {}

        private Lobby(LobbyId id, string name, Player player)
        {
            Id = id;
            Configuration = new LobbyConfiguration(name, MatchMode.OneVsOne, new []{Maps.AimMap});
            _members.Add(new LobbyMember(player.Id, LobbyMemberRole.Leader));
        }

        public static async Task<Lobby> CreateAsync(LobbyId id, string name, Player player, ILobbyRepository lobbyRepository)
        {
            await BusinessRules.CheckAsync(new PlayerMustNotBeMemberOfAnyLobbyRule(lobbyRepository, player.Id));

            return new Lobby(id, name, player);
        }
        
        public void Invite(Player invitingPlayer, Player invitedPlayer)
        {
            BusinessRules.Check(new PlayerMustBeLobbyMemberRule(_members, invitingPlayer.Id));
            BusinessRules.Check(new PlayerMustNotBeLobbyMemberRule(_members, invitedPlayer.Id));
            BusinessRules.Check(new PlayerMustNotBeOnInvitationListRule(_invitations, invitedPlayer.Id));

            var invitation = new LobbyInvitation(invitingPlayer.Id, invitedPlayer.Id, DateTime.UtcNow);

            _invitations.Add(invitation);
        }

        public async Task AcceptInvitationAsync(Player invitedPlayer, ILobbyRepository lobbyRepository)
        {
            BusinessRules.Check(new InvitationMustExistForPlayerRule(_invitations, invitedPlayer.Id));
            BusinessRules.Check(new LobbyMustNotBeFullRule(this));
            await BusinessRules.CheckAsync(new PlayerMustNotBeMemberOfAnyLobbyRule(lobbyRepository, invitedPlayer.Id));

            var invitation = _invitations.FirstOrDefault(i => i.InvitedPlayerId == invitedPlayer.Id);
            if (invitation is not null)
            {
                _invitations.Remove(invitation);

                if (_members.Count == MaxMembers && _invitations.Any())
                {
                    _invitations.Clear();
                }

                _members.Add(new LobbyMember(invitation.InvitedPlayerId, LobbyMemberRole.Normal));
            }
        }

        public void CancelInvitation(Player invitedPlayer)
        {
            var invitation = _invitations.FirstOrDefault(i => i.InvitedPlayerId == invitedPlayer.Id);
            if (invitation is not null)
            {
                _invitations.Remove(invitation);
            }
        }

        public void Leave(PlayerId playerId)
        {
            var member = _members.FirstOrDefault(m => m.PlayerId == playerId);
            if (member is null)
            {
                return;
            }
            
            _members.Remove(member);
            
            AddDomainEvent(new MemberLeftDomainEvent(Id, member));

            if (member.Role == LobbyMemberRole.Leader && _members.Any())
            {
                var first = _members.FirstOrDefault();
                if (first is not null)
                {
                    var updated = first.PromoteToLeader();
                    
                    _members.Remove(first);
                    _members.Add(updated);
                    
                    AddDomainEvent(new MemberRoleChangedDomainEvent(Id, updated));
                }
            }
        }

        public void Kick(PlayerId kickingPlayerId, PlayerId kickedPlayerId)
        {
            BusinessRules.Check(new PlayerMustBeLobbyMemberRule(_members, kickedPlayerId));
            BusinessRules.Check(new PlayerMustBeLobbyLeaderRule(_members, kickingPlayerId));
            BusinessRules.Check(new LobbyLeaderCannotKickHimselfRule(_members, kickingPlayerId, kickedPlayerId));

            var member = _members.FirstOrDefault(m => m.PlayerId == kickedPlayerId);
            if (member is not null)
            {
                _members.Remove(member);
            }
        }

        public void ChangeConfiguration(PlayerId playerId, LobbyConfiguration configuration)
        {
            BusinessRules.Check(new PlayerMustBeLobbyLeaderRule(_members, playerId));
            BusinessRules.Check(new LobbyStatusMustMatchRule(Status, LobbyStatus.Open));
            BusinessRules.Check(new LobbyConfigurationMustBeValidRule(configuration));

            Configuration = configuration;
        }

        public void StartSearching(PlayerId playerId)
        {
            BusinessRules.Check(new PlayerMustBeLobbyLeaderRule(_members, playerId));

            ChangeStatus(LobbyStatus.Searching);
        }

        public void RestoreSearching()
        {
            if (Status != LobbyStatus.Closed) return;
            
            ChangeStatus(LobbyStatus.Searching);
        }

        public void CancelSearching(PlayerId playerId)
        {
            BusinessRules.Check(new PlayerMustBeLobbyLeaderRule(_members, playerId));

            ChangeStatus(LobbyStatus.Open);
        }

        public void Open()
        {
            ChangeStatus(LobbyStatus.Open);
        }

        public void Close()
        {
            ChangeStatus(LobbyStatus.Closed);
        }

        public bool IsFull() => _members.Count == MaxMembers;

        private void ChangeStatus(LobbyStatus status)
        {
            Status = status;
            
            AddDomainEvent(new LobbyStatusChangedDomainEvent(Id, status));
        }
    }
}