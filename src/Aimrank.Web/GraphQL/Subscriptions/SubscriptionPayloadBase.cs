using Aimrank.Web.GraphQL.Queries;
using HotChocolate.Resolvers;

namespace Aimrank.Web.GraphQL.Subscriptions
{
    public record SubscriptionPayloadBase
    {
        public Query GetQuery(IResolverContext context) => context.GetQueryRoot<Query>();
    }
}