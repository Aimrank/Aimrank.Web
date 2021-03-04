namespace Aimrank.Web.Contracts.Users
{
    public class SignInRequest
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}