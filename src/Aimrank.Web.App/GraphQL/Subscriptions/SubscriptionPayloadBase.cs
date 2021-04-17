using Aimrank.Web.App.GraphQL.Queries;
using HotChocolate.Resolvers;

namespace Aimrank.Web.App.GraphQL.Subscriptions
{
    public record SubscriptionPayloadBase
    {
        public Query GetQuery(IResolverContext context) => context.GetQueryRoot<Query>();
    }
}