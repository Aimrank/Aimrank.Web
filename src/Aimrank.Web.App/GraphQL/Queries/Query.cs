using Aimrank.Web.App.GraphQL.Queries.DataLoaders;
using Aimrank.Web.App.GraphQL.Queries.Models;
using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.Modules.Matches.Application.Matches.GetFinishedMatches;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types.Pagination;
using HotChocolate;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.App.GraphQL.Queries
{
    public class Query
    {
        // Users

        [Authorize]
        public Task<User> GetUser(Guid userId, [DataLoader] UserDataLoader loader)
            => loader.LoadAsync(userId, CancellationToken.None);

        [Authorize]
        public async Task<Connection<User>> GetUsers(
            string username,
            int? skip,
            int? take,
            [DataLoader] UserSearchDataLoader loader)
        {
            var input = new UserSearchDataLoaderInput(username, new PaginationQuery(skip, take));
            var result = await loader.LoadAsync(input, CancellationToken.None);

            return result.AsConnection(skip, take);
        }
        
        // Friends

        [Authorize]
        public Task<Friendship> GetFriendship(Guid userId, [DataLoader] FriendshipDataLoader loader)
            => loader.LoadAsync(userId, CancellationToken.None);
        
        [Authorize]
        public async Task<Connection<User>> GetBlockedUsers(
            int? skip,
            int? take,
            [DataLoader] BlockedUsersDataLoader loader)
        {
            var pagination = new PaginationQuery(skip, take);
            var result = await loader.LoadAsync(pagination, CancellationToken.None);

            return result.AsConnection(skip, take);
        }
        
        [Authorize]
        public async Task<Connection<User>> GetFriendshipInvitations(
            int? skip,
            int? take,
            [DataLoader] FriendshipInvitationsDataLoader loader)
        {
            var pagination = new PaginationQuery(skip, take);
            var result = await loader.LoadAsync(pagination, CancellationToken.None);

            return result.AsConnection(skip, take);
        }
        
        // Matches
        
        [Authorize]
        public async Task<Connection<Match>> GetMatches(
            FinishedMatchesFilter filter,
            int? skip,
            int? take,
            [DataLoader] MatchesDataLoader loader)
        {
            var input = new MatchesDataLoaderInput(filter, new PaginationQuery(skip, take));
            var result = await loader.LoadAsync(input, CancellationToken.None);

            return result.AsConnection(skip, take);
        }
        
        // Lobbies

        [Authorize]
        public Task<Lobby> GetLobby([DataLoader] LobbyDataLoader loader)
            => loader.LoadAsync(0, CancellationToken.None);
        
        [Authorize]
        public Task<IEnumerable<LobbyInvitation>> GetLobbyInvitations(
            [DataLoader] LobbyInvitationsDataLoader loader)
            => loader.LoadAsync(0, CancellationToken.None);
        
        
        // Cluster

        [AuthorizeRoles("Admin")]
        public Task<IEnumerable<SteamToken>> GetSteamTokens([DataLoader] SteamTokensDataLoader loader)
            => loader.LoadAsync(0, CancellationToken.None);
    }
}