using Aimrank.Web.App.Configuration.Clients.Cluster;
using Aimrank.Web.Common.Application.Exceptions;
using Aimrank.Web.Common.Domain;
using HotChocolate;
using System.Collections.Generic;
using System.Text.Json;

namespace Aimrank.Web.App.GraphQL
{
    public class GraphQLErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
            => error.Exception switch
            {
                BusinessRuleValidationException e => error
                    .WithCode(e.BrokenRule.Code)
                    .WithMessage(e.BrokenRule.Message),
                ApplicationException e => error
                    .WithCode(e.Code)
                    .WithMessage(e.Message),
                InvalidCommandException e => error
                    .WithCode(e.Code)
                    .WithMessage(e.Message)
                    .SetExtension("errors", JsonSerializer.Serialize(e.Errors)),
                ClusterApiException e => error
                    .WithMessage(e.Message),
                null => error
                    .WithCode(error.Code)
                    .WithMessage(error.Message),
                _ => error
                    .WithMessage("Internal server error")
                    .WithExtensions(new Dictionary<string, object>())
            };
    }
}