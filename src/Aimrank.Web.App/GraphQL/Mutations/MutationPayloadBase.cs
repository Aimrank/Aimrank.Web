using Aimrank.Web.App.GraphQL.Queries;
using HotChocolate.Resolvers;
using HotChocolate;

namespace Aimrank.Web.App.GraphQL.Mutations
{
    public record MutationPayloadBase
    {
        public Query GetQuery(IResolverContext context) => context.GetQueryRoot<Query>();
        
        [GraphQLNonNullType]
        public string Status => "OK";
    }
}