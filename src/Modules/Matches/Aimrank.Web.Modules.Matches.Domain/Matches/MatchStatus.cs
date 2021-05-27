namespace Aimrank.Web.Modules.Matches.Domain.Matches
{
    public enum MatchStatus
    {
        Created,    // Match and server found
        Ready,      // Match is ready but it's waiting for acceptance
        Started,    // Server is started and ready for game
        Finished
    }
}