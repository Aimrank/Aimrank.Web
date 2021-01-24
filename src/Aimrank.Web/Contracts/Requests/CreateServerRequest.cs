namespace Aimrank.Web.Contracts.Requests
{
    public class CreateServerRequest
    {
        public string Token { get; set; }
        public string[] Whitelist { get; set; }
    }
}