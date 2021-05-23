using Aimrank.Web.Modules.UserAccess.Domain.Users;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Domain.Users
{
    internal class PasswordHasher : IPasswordHasher
    {
        private const KeyDerivationPrf Algorithm = KeyDerivationPrf.HMACSHA1;
        private const int SaltBytes = 16;
        private const int PassBytes = 20;
        private const int Iterations = 10000;
        
        public string HashPassword(string password)
        {
            var salt = new byte[SaltBytes];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            var hash = KeyDerivation.Pbkdf2(password, salt, Algorithm, Iterations, PassBytes);
            var hashBytes = new byte[SaltBytes + PassBytes];
            
            Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltBytes);
            Buffer.BlockCopy(hash, 0, hashBytes, SaltBytes, PassBytes);
            
            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string hashedPassword, string password)
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
            
            var salt = new byte[SaltBytes];
            
            Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltBytes);

            var hash = KeyDerivation.Pbkdf2(password, salt, Algorithm, Iterations, PassBytes);

            for (var i = 0; i < PassBytes; i++)
            {
                if (hashBytes[i + SaltBytes] != hash[i])
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}