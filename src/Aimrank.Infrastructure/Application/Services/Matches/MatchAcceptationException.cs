using Aimrank.Common.Application.Exceptions;

namespace Aimrank.Infrastructure.Application.Services.Matches
{
    internal class MatchAcceptationException : ApplicationException
    {
        public override string Code => "match_acceptation_failed";
        
        public MatchAcceptationException() : base("You cannot accept this match")
        {
        }
    }
}