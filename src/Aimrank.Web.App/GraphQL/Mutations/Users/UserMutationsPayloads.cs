using HotChocolate;
using System;

namespace Aimrank.Web.App.GraphQL.Mutations.Users
{
    public record AuthenticationSuccessRecord(
        [GraphQLNonNullType] Guid Id,
        [GraphQLNonNullType] string Username,
        [GraphQLNonNullType] string Email);
    
    public record SignInPayload(AuthenticationSuccessRecord Record) : MutationPayloadBase;
    public record SignUpPayload : MutationPayloadBase;
    
    public record SignOutPayload(bool Record = true) : MutationPayloadBase;

    public record RequestEmailConfirmationPayload : MutationPayloadBase;

    public record RequestPasswordReminderPayload : MutationPayloadBase;

    public record ChangePasswordPayload : MutationPayloadBase;

    public record ResetPasswordPayload : MutationPayloadBase;
}