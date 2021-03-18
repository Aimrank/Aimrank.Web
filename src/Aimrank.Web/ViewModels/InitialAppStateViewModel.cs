namespace Aimrank.Web.ViewModels
{
    public class InitialAppStateViewModel
    {
        public string Error { get; set; }
        public InitialAppStateUser User { get; set; }
    }

    public class InitialAppStateUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}