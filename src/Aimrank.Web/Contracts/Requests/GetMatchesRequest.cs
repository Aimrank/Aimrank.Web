namespace Aimrank.Web.Contracts.Requests
{
    public class GetMatchesRequest : PaginationRequest
    {
        public int? Mode { get; set; }
        public string Map { get; set; }
    }
}