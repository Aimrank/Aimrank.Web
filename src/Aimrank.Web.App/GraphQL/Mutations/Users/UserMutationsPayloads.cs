using HotChocolate.Types;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.App.GraphQL.Mutations.Users
{
    public record AuthenticationSuccessRecord(Guid Id, string Username, string Email, IEnumerable<string> Roles);
    
    public class AuthenticationSuccessRecordType : ObjectType<AuthenticationSuccessRecord>
    {
        protected override void Configure(IObjectTypeDescriptor<AuthenticationSuccessRecord> descriptor)
        {
            descriptor.Field(f => f.Id).Type<NonNullType<UuidType>>();
            descriptor.Field(f => f.Email).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Username).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Roles).Type<NonNullType<ListType<NonNullType<StringType>>>>();
        }
    }
    
    public record SignInPayload(AuthenticationSuccessRecord Record) : MutationPayloadBase;
    public record SignUpPayload : MutationPayloadBase;
    
    public record SignOutPayload(bool Record = true) : MutationPayloadBase;

    public record RequestEmailConfirmationPayload : MutationPayloadBase;

    public record RequestPasswordReminderPayload : MutationPayloadBase;

    public record ChangePasswordPayload : MutationPayloadBase;

    public record ResetPasswordPayload : MutationPayloadBase;
}