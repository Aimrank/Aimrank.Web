namespace Aimrank.Web.Contracts.Requests
{
    public class GetMatchesHistoryRequest : PaginationRequest
    {
        public int? Mode { get; set; }
        public string Map { get; set; }
    }
}