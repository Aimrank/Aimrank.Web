namespace Aimrank.Application.Commands.Users.RefreshJwt
{
    public class AuthenticationSuccessDto
    {
        public string Jwt { get; set; }
        public string RefreshToken { get; set; }
    }
}