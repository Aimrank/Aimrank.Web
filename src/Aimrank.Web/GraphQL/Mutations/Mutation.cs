using Aimrank.Application.Commands.Friendships.AcceptFriendshipInvitation;
using Aimrank.Application.Commands.Friendships.BlockUser;
using Aimrank.Application.Commands.Friendships.DeclineFriendshipInvitation;
using Aimrank.Application.Commands.Friendships.DeleteFriendship;
using Aimrank.Application.Commands.Friendships.InviteUserToFriendsList;
using Aimrank.Application.Commands.Friendships.UnblockUser;
using Aimrank.Application.Commands.Lobbies.AcceptLobbyInvitation;
using Aimrank.Application.Commands.Lobbies.CancelLobbyInvitation;
using Aimrank.Application.Commands.Lobbies.CancelSearchingForGame;
using Aimrank.Application.Commands.Lobbies.ChangeLobbyConfiguration;
using Aimrank.Application.Commands.Lobbies.CreateLobby;
using Aimrank.Application.Commands.Lobbies.InviteUserToLobby;
using Aimrank.Application.Commands.Lobbies.LeaveLobby;
using Aimrank.Application.Commands.Lobbies.StartSearchingForGame;
using Aimrank.Application.Commands.Matches.AcceptMatch;
using Aimrank.Application.Commands.Users.RefreshJwt;
using Aimrank.Application.Commands.Users.SignIn;
using Aimrank.Application.Commands.Users.SignUp;
using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Web.GraphQL.Subscriptions.Messages.Lobbies;
using Aimrank.Web.GraphQL.Subscriptions.Messages.Users;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;

namespace Aimrank.Web.GraphQL.Mutations
{
    public class Mutation
    {
        private readonly IAimrankModule _aimrankModule;
        private readonly ITopicEventSender _topicEventSender;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public Mutation(
            IAimrankModule aimrankModule,
            ITopicEventSender topicEventSender,
            IExecutionContextAccessor executionContextAccessor)
        {
            _aimrankModule = aimrankModule;
            _topicEventSender = topicEventSender;
            _executionContextAccessor = executionContextAccessor;
        }
        
        // Users

        public async Task<SignInPayload> SignIn(SignInCommand command)
            => new(await _aimrankModule.ExecuteCommandAsync(command));

        public async Task<SignUpPayload> SignUp(SignUpCommand command)
            => new(await _aimrankModule.ExecuteCommandAsync(command));

        public async Task<RefreshJwtPayload> RefreshJwt(RefreshJwtCommand command)
            => new(await _aimrankModule.ExecuteCommandAsync(command));
        
        // Friends

        [Authorize]
        public async Task<InviteUserToFriendsListPayload> InviteUserToFriendsList(InviteUserToFriendsListCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);

            await _topicEventSender.SendAsync($"FriendshipInvitationCreated:{command.InvitedUserId}",
                new FriendshipInvitationCreatedMessage(_executionContextAccessor.UserId, command.InvitedUserId));

            return new InviteUserToFriendsListPayload();
        }

        [Authorize]
        public async Task<AcceptFriendshipInvitationPayload> AcceptFriendshipInvitation(
            AcceptFriendshipInvitationCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);
            return new AcceptFriendshipInvitationPayload();
        }

        [Authorize]
        public async Task<DeclineFriendshipInvitationPayload> DeclineFriendshipInvitation(
            DeclineFriendshipInvitationCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);
            return new DeclineFriendshipInvitationPayload();
        }

        [Authorize]
        public async Task<BlockUserPayload> BlockUser(BlockUserCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);
            return new BlockUserPayload();
        }

        [Authorize]
        public async Task<UnblockUserPayload> UnblockUser(UnblockUserCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);
            return new UnblockUserPayload();
        }

        [Authorize]
        public async Task<DeleteFriendshipPayload> DeleteFriendship(DeleteFriendshipCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);
            return new DeleteFriendshipPayload();
        }
        
        // Matches

        [Authorize]
        public async Task<AcceptMatchPayload> AcceptMatch(AcceptMatchCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);
            return new AcceptMatchPayload();
        }
        
        // Lobbies

        [Authorize]
        public async Task<CreateLobbyPayload> CreateLobby(CreateLobbyCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);
            return new CreateLobbyPayload();
        }

        [Authorize]
        public async Task<InviteUserToLobbyPayload> InviteUserToLobby(InviteUserToLobbyCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);

            await _topicEventSender.SendAsync($"LobbyInvitationCreated:{command.InvitedUserId}",
                new LobbyInvitationCreatedMessage(command.LobbyId, _executionContextAccessor.UserId,
                    command.InvitedUserId));

            return new InviteUserToLobbyPayload();
        }

        [Authorize]
        public async Task<AcceptLobbyInvitationPayload> AcceptLobbyInvitation(AcceptLobbyInvitationCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);

            await _topicEventSender.SendAsync($"LobbyInvitationAccepted:{command.LobbyId}",
                new InvitationAcceptedMessage(command.LobbyId, _executionContextAccessor.UserId));

            return new AcceptLobbyInvitationPayload();
        }

        [Authorize]
        public async Task<CancelLobbyInvitationPayload> CancelLobbyInvitation(CancelLobbyInvitationCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);

            await _topicEventSender.SendAsync($"LobbyInvitationCanceled:{command.LobbyId}",
                new InvitationCanceledMessage(command.LobbyId, _executionContextAccessor.UserId));

            return new CancelLobbyInvitationPayload();
        }

        [Authorize]
        public async Task<ChangeLobbyConfigurationPayload> ChangeLobbyConfiguration(ChangeLobbyConfigurationCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);

            await _topicEventSender.SendAsync($"LobbyConfigurationChanged:{command.LobbyId}",
                new LobbyConfigurationChangedMessage(command.LobbyId, command.Map, command.Name, command.Mode));

            return new ChangeLobbyConfigurationPayload();
        }

        [Authorize]
        public async Task<LeaveLobbyPayload> LeaveLobby(LeaveLobbyCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);
            return new LeaveLobbyPayload();
        }

        [Authorize]
        public async Task<StartSearchingForGamePayload> StartSearchingForGame(StartSearchingForGameCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);
            return new StartSearchingForGamePayload();
        }

        [Authorize]
        public async Task<CancelSearchingForGamePayload> CancelSearchingForGame(CancelSearchingForGameCommand command)
        {
            await _aimrankModule.ExecuteCommandAsync(command);
            return new CancelSearchingForGamePayload();
        }
    }
}