namespace Aimrank.Web.Contracts.Requests
{
    public class ChangeLobbyConfigurationRequest
    {
        public string Name { get; set; }
        public string Map { get; set; }
        public int Mode { get; set; }
    }
}