using Aimrank.Domain.Users;

namespace Aimrank.Domain.RefreshTokens
{
    public interface IJwtService
    {
        string CreateJwt(UserId userId, string email);
        UserId GetUserId(string jwt);
        string GetUserEmail(string jwt);
    }
}