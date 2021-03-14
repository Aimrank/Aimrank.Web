using Aimrank.Common.Application;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Application.Lobbies.AcceptLobbyInvitation;
using Aimrank.Modules.Matches.Application.Lobbies.CancelLobbyInvitation;
using Aimrank.Modules.Matches.Application.Lobbies.CancelSearchingForGame;
using Aimrank.Modules.Matches.Application.Lobbies.ChangeLobbyConfiguration;
using Aimrank.Modules.Matches.Application.Lobbies.CreateLobby;
using Aimrank.Modules.Matches.Application.Lobbies.InviteUserToLobby;
using Aimrank.Modules.Matches.Application.Lobbies.LeaveLobby;
using Aimrank.Modules.Matches.Application.Lobbies.StartSearchingForGame;
using Aimrank.Modules.Matches.Application.Matches.AcceptMatch;
using Aimrank.Modules.UserAccess.Application.Authentication.Authenticate;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Friendships.AcceptFriendshipInvitation;
using Aimrank.Modules.UserAccess.Application.Friendships.BlockUser;
using Aimrank.Modules.UserAccess.Application.Friendships.DeclineFriendshipInvitation;
using Aimrank.Modules.UserAccess.Application.Friendships.DeleteFriendship;
using Aimrank.Modules.UserAccess.Application.Friendships.InviteUserToFriendsList;
using Aimrank.Modules.UserAccess.Application.Friendships.UnblockUser;
using Aimrank.Modules.UserAccess.Application.Users.RegisterNewUser;
using Aimrank.Web.GraphQL.Subscriptions.Messages.Lobbies;
using Aimrank.Web.GraphQL.Subscriptions.Messages.Users;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;

namespace Aimrank.Web.GraphQL.Mutations
{
    public class Mutation
    {
        private readonly IMatchesModule _matchesModule;
        private readonly IUserAccessModule _userAccessModule;
        private readonly ITopicEventSender _topicEventSender;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public Mutation(
            IMatchesModule matchesModule,
            IUserAccessModule userAccessModule,
            ITopicEventSender topicEventSender,
            IExecutionContextAccessor executionContextAccessor)
        {
            _matchesModule = matchesModule;
            _userAccessModule = userAccessModule;
            _topicEventSender = topicEventSender;
            _executionContextAccessor = executionContextAccessor;
        }
        
        // Users

        public async Task<SignInPayload> SignIn(AuthenticateCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new SignInPayload();
        }

        public async Task<SignUpPayload> SignUp(RegisterNewUserCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new SignUpPayload();
        }

        public SignOutPayload SignOut() => new SignOutPayload();
        
        // Friends

        [Authorize]
        public async Task<InviteUserToFriendsListPayload> InviteUserToFriendsList(InviteUserToFriendsListCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);

            await _topicEventSender.SendAsync($"FriendshipInvitationCreated:{command.InvitedUserId}",
                new FriendshipInvitationCreatedMessage(_executionContextAccessor.UserId, command.InvitedUserId));

            return new InviteUserToFriendsListPayload();
        }

        [Authorize]
        public async Task<AcceptFriendshipInvitationPayload> AcceptFriendshipInvitation(
            AcceptFriendshipInvitationCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new AcceptFriendshipInvitationPayload();
        }

        [Authorize]
        public async Task<DeclineFriendshipInvitationPayload> DeclineFriendshipInvitation(
            DeclineFriendshipInvitationCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new DeclineFriendshipInvitationPayload();
        }

        [Authorize]
        public async Task<BlockUserPayload> BlockUser(BlockUserCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new BlockUserPayload();
        }

        [Authorize]
        public async Task<UnblockUserPayload> UnblockUser(UnblockUserCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new UnblockUserPayload();
        }

        [Authorize]
        public async Task<DeleteFriendshipPayload> DeleteFriendship(DeleteFriendshipCommand command)
        {
            await _userAccessModule.ExecuteCommandAsync(command);
            return new DeleteFriendshipPayload();
        }
        
        // Matches

        [Authorize]
        public async Task<AcceptMatchPayload> AcceptMatch(AcceptMatchCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);
            return new AcceptMatchPayload();
        }
        
        // Lobbies

        [Authorize]
        public async Task<CreateLobbyPayload> CreateLobby(CreateLobbyCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);
            return new CreateLobbyPayload();
        }

        [Authorize]
        public async Task<InviteUserToLobbyPayload> InviteUserToLobby(InviteUserToLobbyCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);

            await _topicEventSender.SendAsync($"LobbyInvitationCreated:{command.InvitedUserId}",
                new LobbyInvitationCreatedMessage(command.LobbyId, _executionContextAccessor.UserId,
                    command.InvitedUserId));

            return new InviteUserToLobbyPayload();
        }

        [Authorize]
        public async Task<AcceptLobbyInvitationPayload> AcceptLobbyInvitation(AcceptLobbyInvitationCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);

            await _topicEventSender.SendAsync($"LobbyInvitationAccepted:{command.LobbyId}",
                new InvitationAcceptedMessage(command.LobbyId, _executionContextAccessor.UserId));

            return new AcceptLobbyInvitationPayload();
        }

        [Authorize]
        public async Task<CancelLobbyInvitationPayload> CancelLobbyInvitation(CancelLobbyInvitationCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);

            await _topicEventSender.SendAsync($"LobbyInvitationCanceled:{command.LobbyId}",
                new InvitationCanceledMessage(command.LobbyId, _executionContextAccessor.UserId));

            return new CancelLobbyInvitationPayload();
        }

        [Authorize]
        public async Task<ChangeLobbyConfigurationPayload> ChangeLobbyConfiguration(ChangeLobbyConfigurationCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);

            await _topicEventSender.SendAsync($"LobbyConfigurationChanged:{command.LobbyId}",
                new LobbyConfigurationChangedMessage(command.LobbyId, command.Map, command.Name, command.Mode));

            return new ChangeLobbyConfigurationPayload();
        }

        [Authorize]
        public async Task<LeaveLobbyPayload> LeaveLobby(LeaveLobbyCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);
            return new LeaveLobbyPayload();
        }

        [Authorize]
        public async Task<StartSearchingForGamePayload> StartSearchingForGame(StartSearchingForGameCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);
            return new StartSearchingForGamePayload();
        }

        [Authorize]
        public async Task<CancelSearchingForGamePayload> CancelSearchingForGame(CancelSearchingForGameCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);
            return new CancelSearchingForGamePayload();
        }
    }
}