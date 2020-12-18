using Application;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Helpers
{
    public class PasswordEncryption : IPasswordEncryption
    {
        private PasswordHasher<IUser> _passwordHasher = new PasswordHasher<IUser>();
        public string HashPassword(IUser user, string providedPassword)
        {
            return _passwordHasher.HashPassword(user, providedPassword);
        }

        public bool VerifyPassword(IUser user, string hashedPassword, string providedPassword)
        {
            bool verified = false;

            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);

            if (result == PasswordVerificationResult.Success) verified = true;
            else if (result == PasswordVerificationResult.SuccessRehashNeeded) verified = true;
            else if (result == PasswordVerificationResult.Failed) verified = false;

            return verified;
        }
    }
}