using System.Security.Cryptography;
using System;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users
{
    public static class PasswordManager
    {
        private const int Iterations = 1000;
        
        public static string HashPassword(string password)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            using var bytes = new Rfc2898DeriveBytes(password, 16, Iterations);

            var salt = bytes.Salt;
            var hash = bytes.GetBytes(20);

            var hashBytes = new byte[36];
            
            Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
            Buffer.BlockCopy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string hashedPassword, string password)
        {
            if (hashedPassword is null)
            {
                return false;
            }

            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var hashBytes = Convert.FromBase64String(hashedPassword);

            var salt = new byte[16];
            
            Buffer.BlockCopy(hashBytes, 0, salt, 0, 16);

            using var bytes = new Rfc2898DeriveBytes(password, salt, Iterations);

            var hash = bytes.GetBytes(20);

            for (var i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}