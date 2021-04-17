using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Application.Lobbies.AcceptLobbyInvitation;
using Aimrank.Web.Modules.Matches.Application.Lobbies.CancelLobbyInvitation;
using Aimrank.Web.Modules.Matches.Application.Lobbies.CancelSearchingForGame;
using Aimrank.Web.Modules.Matches.Application.Lobbies.ChangeLobbyConfiguration;
using Aimrank.Web.Modules.Matches.Application.Lobbies.CreateLobby;
using Aimrank.Web.Modules.Matches.Application.Lobbies.InvitePlayerToLobby;
using Aimrank.Web.Modules.Matches.Application.Lobbies.KickPlayerFromLobby;
using Aimrank.Web.Modules.Matches.Application.Lobbies.LeaveLobby;
using Aimrank.Web.Modules.Matches.Application.Lobbies.StartSearchingForGame;
using Aimrank.Web.Modules.Matches.Application.Matches.AcceptMatch;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies;
using Aimrank.Web.App.GraphQL.Subscriptions.Users.Payloads;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using HotChocolate;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.App.GraphQL.Mutations.Lobbies
{
    [ExtendObjectType("Mutation")]
    public class LobbyMutations
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IMatchesModule _matchesModule;
        private readonly ITopicEventSender _topicEventSender;
        private readonly LobbyEventSender _lobbyEventSender;

        public LobbyMutations(
            IExecutionContextAccessor executionContextAccessor,
            IMatchesModule matchesModule,
            ITopicEventSender topicEventSender,
            LobbyEventSender lobbyEventSender)
        {
            _executionContextAccessor = executionContextAccessor;
            _matchesModule = matchesModule;
            _topicEventSender = topicEventSender;
            _lobbyEventSender = lobbyEventSender;
        }


        [Authorize]
        public async Task<CreateLobbyPayload> CreateLobby()
        {
            var lobbyId = Guid.NewGuid();
            await _matchesModule.ExecuteCommandAsync(new CreateLobbyCommand(lobbyId));
            return new CreateLobbyPayload(lobbyId);
        }
        
        [Authorize]
        public async Task<ChangeLobbyConfigurationPayload> ChangeLobbyConfiguration(
            [GraphQLNonNullType] ChangeLobbyConfigurationCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);

            var payload = new LobbyConfigurationChangedPayload(new LobbyConfigurationChangedRecord(
                input.LobbyId, input.Mode, input.Name, input.Maps));
            
            await _lobbyEventSender.SendAsync("LobbyConfigurationChanged", input.LobbyId, payload);

            return new ChangeLobbyConfigurationPayload();
        }

        [Authorize]
        public async Task<InvitePlayerToLobbyPayload> InvitePlayerToLobby(
            [GraphQLNonNullType] InvitePlayerToLobbyCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);

            await _topicEventSender.SendAsync($"LobbyInvitationCreated:{input.InvitedPlayerId}",
                new LobbyInvitationCreatedPayload(
                    new LobbyInvitationCreatedRecord(
                        input.LobbyId, _executionContextAccessor.UserId)));

            return new InvitePlayerToLobbyPayload();
        }
        
        [Authorize]
        public async Task<KickPlayerFromLobbyPayload> KickPlayerFromLobby(
            [GraphQLNonNullType] KickPlayerFromLobbyCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);

            var payload = new LobbyMemberKickedPayload(new LobbyMemberKickedRecord(input.LobbyId, input.PlayerId));

            await _lobbyEventSender.SendAsync("LobbyMemberKicked", input.LobbyId, payload);
            await _topicEventSender.SendAsync($"LobbyMemberKicked:{input.LobbyId}:{input.PlayerId}", payload);
            
            return new KickPlayerFromLobbyPayload();
        }

        [Authorize]
        public async Task<AcceptLobbyInvitationPayload> AcceptLobbyInvitation(
            [GraphQLNonNullType] AcceptLobbyInvitationCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);

            var payload = new LobbyInvitationAcceptedPayload(new LobbyInvitationAcceptedRecord(
                input.LobbyId, _executionContextAccessor.UserId));
            
            await _lobbyEventSender.SendAsync("LobbyInvitationAccepted", input.LobbyId, payload);

            return new AcceptLobbyInvitationPayload();
        }

        [Authorize]
        public async Task<CancelLobbyInvitationPayload> CancelLobbyInvitation(
            [GraphQLNonNullType] CancelLobbyInvitationCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);
            return new CancelLobbyInvitationPayload();
        }

        [Authorize]
        public async Task<LeaveLobbyPayload> LeaveLobby(
            [GraphQLNonNullType] LeaveLobbyCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);
            return new LeaveLobbyPayload();
        }

        [Authorize]
        public async Task<StartSearchingForGamePayload> StartSearchingForGame(
            [GraphQLNonNullType] StartSearchingForGameCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);
            return new StartSearchingForGamePayload();
        }

        [Authorize]
        public async Task<CancelSearchingForGamePayload> CancelSearchingForGame(
            [GraphQLNonNullType] CancelSearchingForGameCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);
            return new CancelSearchingForGamePayload();
        }
        
        [Authorize]
        public async Task<AcceptMatchPayload> AcceptMatch(
            [GraphQLNonNullType] AcceptMatchCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);
            return new AcceptMatchPayload();
        }
    }
}