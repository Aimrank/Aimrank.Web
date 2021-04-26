using Aimrank.Web.App.GraphQL.Mutations.Friendships;
using Aimrank.Web.App.GraphQL.Mutations.Lobbies;
using Aimrank.Web.App.GraphQL.Mutations.SteamTokens;
using Aimrank.Web.App.GraphQL.Mutations.Users;
using Aimrank.Web.App.GraphQL.Mutations;
using Aimrank.Web.App.GraphQL.Queries;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies;
using Aimrank.Web.App.GraphQL.Subscriptions.Users;
using Aimrank.Web.App.GraphQL.Subscriptions;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Aimrank.Web.App.GraphQL
{
    public static class GraphQLExtensions
    {
        public static IServiceCollection AddApplicationGraphQL(this IServiceCollection services)
        {
            services.AddSingleton<LobbyEventSender>();
            
            services
                .AddGraphQLServer()
                .AddQueryType<QueryType>()
                .AddTypesFromAssembly(Assembly.GetExecutingAssembly())
                .AddMutationType<Mutation>()
                .AddType<UserMutations>()
                .AddType<LobbyMutations>()
                .AddType<FriendshipMutations>()
                .AddType<SteamTokensMutations>()
                .AddSubscriptionType<Subscription>()
                .AddType<UserSubscriptions>()
                .AddType<LobbySubscriptions>()
                .AddErrorFilter<GraphQLErrorFilter>()
                .AddInMemorySubscriptions()
                .AddMaxExecutionDepthRule(20)
                .AddAuthorization();

            return services;
        }

        private static IRequestExecutorBuilder AddTypesFromAssembly(this IRequestExecutorBuilder builder, Assembly assembly)
        {
            var types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ObjectType).IsAssignableFrom(t));

            foreach (var type in types)
            {
                builder.AddType(type);
            }

            return builder;
        }
    }
}