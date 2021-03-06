using Aimrank.Common.Domain;
using Aimrank.Domain.Lobbies.Events;
using Aimrank.Domain.Lobbies.Rules;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Domain.Lobbies
{
    public class Lobby : Entity, IAggregateRoot
    {
        private const int MaxMembers = 2;
        
        public LobbyId Id { get; }
        public LobbyStatus Status { get; private set; }
        private LobbyConfiguration _configuration;
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

        private Lobby(LobbyId id, User user)
        {
            BusinessRules.Check(new UserMustHaveConnectedSteamRule(user));
            
            Id = id;
            _configuration = new LobbyConfiguration($"team_{user.Username}", Maps.AimMap, MatchMode.OneVsOne);
            _members.Add(new LobbyMember(user.Id, LobbyMemberRole.Leader));
        }

        public static async Task<Lobby> CreateAsync(LobbyId id, User user, ILobbyRepository lobbyRepository)
        {
            await BusinessRules.CheckAsync(new UserMustNotBeMemberOfAnyLobbyRule(lobbyRepository, user.Id));

            return new Lobby(id, user);
        }
        
        public void Invite(User invitingUser, User invitedUser)
        {
            BusinessRules.Check(new UserMustHaveConnectedSteamRule(invitedUser));
            BusinessRules.Check(new UserMustBeLobbyMemberRule(_members, invitingUser));
            BusinessRules.Check(new UserMustNotBeLobbyMemberRule(_members, invitedUser));
            BusinessRules.Check(new UserMustNotBeOnInvitationListRule(_invitations, invitedUser));

            var invitation = new LobbyInvitation(invitingUser.Id, invitedUser.Id, DateTime.UtcNow);

            _invitations.Add(invitation);
        }

        public async Task AcceptInvitationAsync(User invitedUser, ILobbyRepository lobbyRepository)
        {
            BusinessRules.Check(new InvitationMustExistForUserRule(_invitations, invitedUser.Id));
            BusinessRules.Check(new UserMustHaveConnectedSteamRule(invitedUser));
            BusinessRules.Check(new LobbyMustNotBeFullRule(this));
            await BusinessRules.CheckAsync(new UserMustNotBeMemberOfAnyLobbyRule(lobbyRepository, invitedUser.Id));

            var invitation = _invitations.FirstOrDefault(i => i.InvitedUserId == invitedUser.Id);
            if (invitation is not null)
            {
                _invitations.Remove(invitation);

                if (_members.Count == MaxMembers && _invitations.Any())
                {
                    _invitations.Clear();
                }

                _members.Add(new LobbyMember(invitation.InvitedUserId, LobbyMemberRole.Normal));
            }
        }

        public void CancelInvitation(User invitedUser)
        {
            var invitation = _invitations.FirstOrDefault(i => i.InvitedUserId == invitedUser.Id);
            if (invitation is not null)
            {
                _invitations.Remove(invitation);
            }
        }

        public void Leave(UserId userId)
        {
            var member = _members.FirstOrDefault(m => m.UserId == userId);
            if (member is null)
            {
                return;
            }
            
            _members.Remove(member);
            
            AddDomainEvent(new MemberLeftDomainEvent(this, member));

            if (member.Role == LobbyMemberRole.Leader && _members.Any())
            {
                var first = _members.FirstOrDefault();
                if (first is not null)
                {
                    var updated = first.PromoteToLeader();
                    
                    _members.Remove(first);
                    _members.Add(updated);
                    
                    AddDomainEvent(new MemberRoleChangedDomainEvent(this, updated));
                }
            }
        }

        public void ChangeConfiguration(UserId userId, LobbyConfiguration configuration)
        {
            BusinessRules.Check(new UserMustBeLobbyLeaderRule(_members, userId));
            BusinessRules.Check(new LobbyStatusMustMatchRule(Status, LobbyStatus.Open));
            BusinessRules.Check(new MapMustBeSupportedRule(configuration.Map));

            _configuration = configuration;
        }

        public void StartSearching(UserId userId)
        {
            BusinessRules.Check(new UserMustBeLobbyLeaderRule(_members, userId));

            ChangeStatus(LobbyStatus.Searching);
        }

        public void RestoreSearching()
        {
            if (Status != LobbyStatus.Closed) return;
            
            ChangeStatus(LobbyStatus.Searching);
        }

        public void CancelSearching(UserId userId)
        {
            BusinessRules.Check(new UserMustBeLobbyLeaderRule(_members, userId));

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
        
        public string GetMap() => _configuration.Map;
        
        public MatchMode GetMode() => _configuration.Mode;

        private void ChangeStatus(LobbyStatus status)
        {
            Status = status;
            
            AddDomainEvent(new LobbyStatusChangedDomainEvent(this));
        }
    }
}