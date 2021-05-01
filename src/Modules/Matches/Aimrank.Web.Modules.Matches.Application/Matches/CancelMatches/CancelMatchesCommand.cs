using Aimrank.Web.Modules.Matches.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Matches.CancelMatches
{
    public class CancelMatchesCommand : ICommand
    {
        public IEnumerable<Guid> Ids { get; }

        public CancelMatchesCommand(IEnumerable<Guid> ids)
        {
            Ids = ids;
        }
    }
}