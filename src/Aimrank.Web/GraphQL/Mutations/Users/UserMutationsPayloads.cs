using HotChocolate;
using System;

namespace Aimrank.Web.GraphQL.Mutations.Users
{
    public record AuthenticationSuccessRecord(
        [GraphQLNonNullType] Guid Id,
        [GraphQLNonNullType] string Username,
        [GraphQLNonNullType] string Email);
    
    public record SignInPayload(AuthenticationSuccessRecord Record) : MutationPayloadBase;
    public record SignUpPayload : MutationPayloadBase;
    
    public record SignOutPayload(bool Record = true) : MutationPayloadBase;
}