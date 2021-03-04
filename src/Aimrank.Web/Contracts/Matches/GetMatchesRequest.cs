namespace Aimrank.Web.Contracts.Matches
{
    public class GetMatchesRequest : PaginationRequest
    {
        public int? Mode { get; set; }
        public string Map { get; set; }
    }
}