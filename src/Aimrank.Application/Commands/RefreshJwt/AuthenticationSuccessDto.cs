namespace Aimrank.Application.Commands.RefreshJwt
{
    public class AuthenticationSuccessDto
    {
        public string Jwt { get; set; }
        public string RefreshToken { get; set; }
    }
}