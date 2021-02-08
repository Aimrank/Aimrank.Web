using Aimrank.Application.Commands.Lobbies.AcceptLobbyInvitation;
using Aimrank.Application.Commands.Lobbies.CancelLobbyInvitation;
using Aimrank.Application.Commands.Lobbies.ChangeLobbyConfiguration;
using Aimrank.Application.Commands.Lobbies.CreateLobby;
using Aimrank.Application.Commands.Lobbies.InviteUserToLobby;
using Aimrank.Application.Commands.Lobbies.LeaveLobby;
using Aimrank.Application.Commands.Lobbies.StartSearchingForGame;
using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetLobbyForUser;
using Aimrank.Application.Queries.GetLobbyInvitations;
using Aimrank.Application.Queries.GetMatchForLobby;
using Aimrank.Common.Application;
using Aimrank.Web.Attributes;
using Aimrank.Web.Contracts.Requests;
using Aimrank.Web.Hubs.General;
using Aimrank.Web.Hubs.Lobbies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Controllers
{
    [JwtAuth]
    [ApiController]
    [Route("api/[controller]")]
    public class LobbyController : ControllerBase
    {
        private readonly IAimrankModule _aimrankModule;
        private readonly IHubContext<GeneralHub, IGeneralClient> _generalHubContext;
        private readonly IHubContext<LobbyHub, ILobbyClient> _lobbyHubContext;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public LobbyController(
            IAimrankModule aimrankModule,
            IHubContext<GeneralHub, IGeneralClient> generalHubContext,
            IHubContext<LobbyHub, ILobbyClient> lobbyHubContext,
            IExecutionContextAccessor executionContextAccessor)
        {
            _aimrankModule = aimrankModule;
            _generalHubContext = generalHubContext;
            _lobbyHubContext = lobbyHubContext;
            _executionContextAccessor = executionContextAccessor;
        }

        [HttpGet("current")]
        public async Task<ActionResult<LobbyDto>> GetForCurrentUser()
        {
            var lobby = await _aimrankModule.ExecuteQueryAsync(new GetLobbyForUserQuery());
            if (lobby is null)
            {
                return NoContent();
            }
            
            return lobby;
        }

        [HttpGet("invitations")]
        public async Task<ActionResult<IEnumerable<LobbyInvitationDto>>> GetInvitations()
        {
            var invitations = await _aimrankModule.ExecuteQueryAsync(new GetLobbyInvitationsQuery());
            return Ok(invitations);
        }

        [HttpGet("{id}/match")]
        public async Task<ActionResult<MatchDto>> GetMatch(Guid id)
        {
            var match = await _aimrankModule.ExecuteQueryAsync(new GetMatchForLobbyQuery(id));
            if (match is null)
            {
                return NoContent();
            }

            return match;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await _aimrankModule.ExecuteCommandAsync(new CreateLobbyCommand(Guid.NewGuid()));
            return Ok();
        }

        [HttpPost("{id}/invite")]
        public async Task<IActionResult> Invite(Guid id, InviteUserToLobbyRequest request)
        {
            await _aimrankModule.ExecuteCommandAsync(new InviteUserToLobbyCommand(id, request.InvitedUserId));

            var @event = new InvitationCreatedEvent(id, _executionContextAccessor.UserId, request.InvitedUserId);

            await _lobbyHubContext.Clients.Group(id.ToString()).InvitationCreated(@event);
            await _generalHubContext.Clients.User(request.InvitedUserId.ToString()).InvitationCreated(@event);
            
            return Ok();
        }

        [HttpPost("{id}/invite/accept")]
        public async Task<IActionResult> AcceptInvitation(Guid id)
        {
            await _aimrankModule.ExecuteCommandAsync(new AcceptLobbyInvitationCommand(id));

            await _lobbyHubContext.Clients.Group(id.ToString())
                .InvitationAccepted(new InvitationAcceptedEvent(id, _executionContextAccessor.UserId));
            
            return Ok();
        }

        [HttpPost("{id}/invite/cancel")]
        public async Task<IActionResult> CancelInvitation(Guid id)
        {
            await _aimrankModule.ExecuteCommandAsync(new CancelLobbyInvitationCommand(id));
            
            await _lobbyHubContext.Clients.Group(id.ToString())
                .InvitationCanceled(new InvitationCanceledEvent(id, _executionContextAccessor.UserId));
            
            return Ok();
        }

        [HttpDelete("{id}/members")]
        public async Task<IActionResult> Leave(Guid id)
        {
            await _aimrankModule.ExecuteCommandAsync(new LeaveLobbyCommand(id));
            return Ok();
        }

        [HttpPost("{id}/configuration")]
        public async Task<IActionResult> ChangeConfiguration(Guid id, ChangeLobbyConfigurationRequest request)
        {
            await _aimrankModule.ExecuteCommandAsync(new ChangeLobbyConfigurationCommand(
                id,
                request.Map,
                request.Name,
                request.Mode));
            
            return Ok();
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> StartSearching(Guid id)
        {
            await _aimrankModule.ExecuteCommandAsync(new StartSearchingForGameCommand(id));
            return Ok();
        }
    }
}