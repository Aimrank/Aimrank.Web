using Aimrank.Web.GraphQL.Mutations.Friendships;
using Aimrank.Web.GraphQL.Mutations.Lobbies;
using Aimrank.Web.GraphQL.Mutations.Users;
using Aimrank.Web.GraphQL.Mutations;
using Aimrank.Web.GraphQL.Queries;
using Aimrank.Web.GraphQL.Subscriptions.Lobbies;
using Aimrank.Web.GraphQL.Subscriptions.Users;
using Aimrank.Web.GraphQL.Subscriptions;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Aimrank.Web.GraphQL
{
    public static class GraphQLExtensions
    {
        public static IServiceCollection AddApplicationGraphQL(this IServiceCollection services)
        {
            services.AddScoped<LobbyEventSender>();
            
            services
                .AddGraphQLServer()
                .AddQueryType<QueryType>()
                .AddTypesFromAssembly(Assembly.GetExecutingAssembly())
                .AddMutationType<Mutation>()
                .AddType<UserMutations>()
                .AddType<LobbyMutations>()
                .AddType<FriendshipMutations>()
                .AddSubscriptionType<Subscription>()
                .AddType<UserSubscriptions>()
                .AddType<LobbySubscriptions>()
                .AddErrorFilter<GraphQLErrorFilter>()
                .AddInMemorySubscriptions()
                .AddMaxExecutionDepthRule(8)
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