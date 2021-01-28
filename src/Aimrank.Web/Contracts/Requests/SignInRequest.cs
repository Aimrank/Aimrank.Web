namespace Aimrank.Web.Contracts.Requests
{
    public class SignInRequest
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}