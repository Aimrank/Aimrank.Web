namespace Aimrank.Domain.RefreshTokens
{
    public interface IJwtService
    {
        string CreateJwt(string userId, string email);
        string GetUserId(string jwt);
        string GetUserEmail(string jwt);
    }
}