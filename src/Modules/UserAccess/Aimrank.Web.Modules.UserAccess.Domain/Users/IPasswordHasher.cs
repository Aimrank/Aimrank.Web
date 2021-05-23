namespace Aimrank.Web.Modules.UserAccess.Domain.Users
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string password);
    }
}