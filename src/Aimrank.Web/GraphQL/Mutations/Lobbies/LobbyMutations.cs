using Aimrank.Common.Application;
using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Application.Lobbies.AcceptLobbyInvitation;
using Aimrank.Modules.Matches.Application.Lobbies.CancelLobbyInvitation;
using Aimrank.Modules.Matches.Application.Lobbies.CancelSearchingForGame;
using Aimrank.Modules.Matches.Application.Lobbies.ChangeLobbyConfiguration;
using Aimrank.Modules.Matches.Application.Lobbies.CreateLobby;
using Aimrank.Modules.Matches.Application.Lobbies.InvitePlayerToLobby;
using Aimrank.Modules.Matches.Application.Lobbies.LeaveLobby;
using Aimrank.Modules.Matches.Application.Lobbies.StartSearchingForGame;
using Aimrank.Modules.Matches.Application.Matches.AcceptMatch;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads;
using Aimrank.Web.GraphQL.Subscriptions.Users.Payloads;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using HotChocolate;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.GraphQL.Mutations.Lobbies
{
    [ExtendObjectType("Mutation")]
    public class LobbyMutations
    {
        private readonly IMatchesModule _matchesModule;
        private readonly ITopicEventSender _sender;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public LobbyMutations(
            IMatchesModule matchesModule,
            ITopicEventSender sender,
            IExecutionContextAccessor executionContextAccessor)
        {
            _matchesModule = matchesModule;
            _sender = sender;
            _executionContextAccessor = executionContextAccessor;
        }

        [Authorize]
        public async Task<CreateLobbyPayload> CreateLobby()
        {
            var lobbyId = Guid.NewGuid();
            await _matchesModule.ExecuteCommandAsync(new CreateLobbyCommand(lobbyId));
            return new CreateLobbyPayload(lobbyId);
        }

        [Authorize]
        public async Task<InviteUserToLobbyPayload> InviteUserToLobby(
            [GraphQLNonNullType] InvitePlayerToLobbyCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);

            await _sender.SendAsync($"LobbyInvitationCreated:{input.InvitedPlayerId}",
                new LobbyInvitationCreatedPayload(
                    new LobbyInvitationCreatedRecord(
                        input.LobbyId, _executionContextAccessor.UserId)));

            return new InviteUserToLobbyPayload();
        }

        [Authorize]
        public async Task<AcceptLobbyInvitationPayload> AcceptLobbyInvitation(
            [GraphQLNonNullType] AcceptLobbyInvitationCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);

            await _sender.SendAsync($"LobbyInvitationAccepted:{input.LobbyId}",
                new LobbyInvitationAcceptedPayload(
                    new LobbyInvitationAcceptedRecord(input.LobbyId, _executionContextAccessor.UserId)));

            return new AcceptLobbyInvitationPayload();
        }

        [Authorize]
        public async Task<CancelLobbyInvitationPayload> CancelLobbyInvitation(
            [GraphQLNonNullType] CancelLobbyInvitationCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);

            await _sender.SendAsync($"LobbyInvitationCanceled:{input.LobbyId}",
                new LobbyInvitationCanceledPayload(
                    new LobbyInvitationCanceledRecord(input.LobbyId, _executionContextAccessor.UserId)));

            return new CancelLobbyInvitationPayload();
        }

        [Authorize]
        public async Task<ChangeLobbyConfigurationPayload> ChangeLobbyConfiguration(
            [GraphQLNonNullType] ChangeLobbyConfigurationCommand input)
        {
            await _matchesModule.ExecuteCommandAsync(input);

            await _sender.SendAsync($"LobbyConfigurationChanged:{input.LobbyId}",
                new LobbyConfigurationChangedPayload(
                    new LobbyConfigurationChangedRecord(input.LobbyId, input.Map, input.Name, input.Mode)));

            return new ChangeLobbyConfigurationPayload();
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