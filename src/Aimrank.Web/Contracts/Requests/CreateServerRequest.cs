namespace Aimrank.Web.Contracts.Requests
{
    public class CreateServerRequest
    {
        public string Map { get; set; }
        public string[] Whitelist { get; set; }
    }
}