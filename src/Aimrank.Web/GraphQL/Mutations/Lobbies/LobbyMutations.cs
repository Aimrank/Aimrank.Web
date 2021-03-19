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
        public async Task<InviteUserToLobbyPayload> InviteUserToLobby(InvitePlayerToLobbyCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);

            await _sender.SendAsync($"LobbyInvitationCreated:{command.InvitedPlayerId}",
                new LobbyInvitationCreatedPayload(
                    new LobbyInvitationCreatedRecord(
                        command.LobbyId, _executionContextAccessor.UserId)));

            return new InviteUserToLobbyPayload();
        }

        [Authorize]
        public async Task<AcceptLobbyInvitationPayload> AcceptLobbyInvitation(AcceptLobbyInvitationCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);

            await _sender.SendAsync($"LobbyInvitationAccepted:{command.LobbyId}",
                new LobbyInvitationAcceptedPayload(
                    new LobbyInvitationAcceptedRecord(command.LobbyId, _executionContextAccessor.UserId)));

            return new AcceptLobbyInvitationPayload();
        }

        [Authorize]
        public async Task<CancelLobbyInvitationPayload> CancelLobbyInvitation(CancelLobbyInvitationCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);

            await _sender.SendAsync($"LobbyInvitationCanceled:{command.LobbyId}",
                new LobbyInvitationCanceledPayload(
                    new LobbyInvitationCanceledRecord(command.LobbyId, _executionContextAccessor.UserId)));

            return new CancelLobbyInvitationPayload();
        }

        [Authorize]
        public async Task<ChangeLobbyConfigurationPayload> ChangeLobbyConfiguration(ChangeLobbyConfigurationCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);

            await _sender.SendAsync($"LobbyConfigurationChanged:{command.LobbyId}",
                new LobbyConfigurationChangedPayload(
                    new LobbyConfigurationChangedRecord(command.LobbyId, command.Map, command.Name, command.Mode)));

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
        
        [Authorize]
        public async Task<AcceptMatchPayload> AcceptMatch(AcceptMatchCommand command)
        {
            await _matchesModule.ExecuteCommandAsync(command);
            return new AcceptMatchPayload();
        }
    }
}