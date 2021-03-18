using Aimrank.Web.GraphQL.Queries;
using HotChocolate.Resolvers;

namespace Aimrank.Web.GraphQL.Mutations
{
    public record MutationPayloadBase
    {
        public Query GetQuery(IResolverContext context) => context.GetQueryRoot<Query>();
    }
}