using Aimrank.Application.Commands.Users.SignIn;
using Aimrank.Application.Commands.Users.SignUp;
using Aimrank.Common.Application.Exceptions;
using Aimrank.Common.Domain;
using HotChocolate;
using System.Text.Json;

namespace Aimrank.Web.GraphQL
{
    public class GraphQLErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
            => error.Exception switch
            {
                BusinessRuleValidationException e => error
                    .WithCode(e.BrokenRule.Code)
                    .WithMessage(e.BrokenRule.Message),
                SignUpException e => error
                    .WithCode(e.Code)
                    .WithMessage(e.Message)
                    .SetExtension("errors", JsonSerializer.Serialize(e.Errors)),
                SignInException e => error
                    .WithCode(e.Code)
                    .WithMessage(e.Message),
                ApplicationException e => error
                    .WithCode(e.Code)
                    .WithMessage(e.Message),
                InvalidCommandException e => error
                    .WithCode(e.Code)
                    .WithMessage(e.Message)
                    .SetExtension("errors", JsonSerializer.Serialize(e.Errors)),
                _ => error
            };
    }
}