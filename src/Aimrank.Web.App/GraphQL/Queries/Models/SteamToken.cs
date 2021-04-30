using Aimrank.Web.Modules.Matches.Application.Clients;
using HotChocolate.Types;

namespace Aimrank.Web.App.GraphQL.Queries.Models
{
    public class SteamToken
    {
        public string Token { get; }
        public bool IsUsed { get; }

        public SteamToken(SteamTokenDto dto)
        {
            Token = dto.Token;
            IsUsed = dto.IsUsed;
        }
    }

    public class SteamTokenType : ObjectType<SteamToken>
    {
        protected override void Configure(IObjectTypeDescriptor<SteamToken> descriptor)
        {
            descriptor.Field(f => f.Token).Type<NonNullType<StringType>>();
        }
    }
}