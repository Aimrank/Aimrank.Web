namespace Aimrank.Domain.Matches
{
    public enum MatchStatus
    {
        Created,    // Match and server found
        Ready,      // Match is ready but it's waiting for acceptance
        Starting,   // Match was accepted => Match was assigned to server and server is starting
        Started,    // Server is started and ready for game
        Finished
    }
}